using Azure.Core;
using EMR_AIPredictionSystem.Data;
using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Request.MedicalFile;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.MedicalFiles;
using EMR_AIPredictionSystem.Model.Response.MedicalRecord;
using EMR_AIPredictionSystem.Model.Response.Patient;
using EMR_AIPredictionSystem.Model.Response.User;
using EMR_AIPredictionSystem.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EMR_AIPredictionSystem.Service
{
    public class MedicalFileService : IMedicalFileService
    {
        private readonly ApplicationDbContext _context;
        public MedicalFileService(ApplicationDbContext context)
        {
            _context = context;
        }
        private string GenerateMedicalFileId()
        {
            string prefix = "MF-";
            int maxIdNumber = 0;
            var existingIds = _context.MedicalFiles
                .Where(p => p.Id.StartsWith(prefix))
                .Select(p => p.Id)
                .ToList();
            foreach (var id in existingIds)
            {
                if (int.TryParse(id.Substring(prefix.Length), out int idNumber))
                {
                    if (idNumber > maxIdNumber)
                    {
                        maxIdNumber = idNumber;
                    }
                }
            }
            return $"{prefix}{(maxIdNumber + 1).ToString("D8")}";
        }
        private async Task<MedicalFileResponse> GetMedicalFile(string id)
        {
            var response = new MedicalFileResponse();
            var file = await _context.MedicalFiles
                .Include(f => f.FileCategory)
                .Include(f => f.CreatedByNavigation)
                    .ThenInclude(u => u.Person)
                .Include(f => f.Patient)
                    .ThenInclude(p => p.Person)
                        .ThenInclude(person => person.User)
                .Include(f => f.MedicalDocuments)
                    .ThenInclude(d => d.DocumentType)
                .Include(f => f.MedicalDocuments)
                    .ThenInclude(d => d.MedicalRecord)
                        .ThenInclude(r => r.ClinicalVital)
                .Include(f => f.MedicalDocuments)
                    .ThenInclude(d => d.MedicalRecord)
                        .ThenInclude(r => r.LabResults)
                .Include(f => f.MedicalDocuments)
                    .ThenInclude(d => d.MedicalRecord)
                        .ThenInclude(r => r.Doctor)
                            .ThenInclude(doc => doc.User)
                                .ThenInclude(u => u.Person)
                .Include(f => f.MedicalDocuments)
                    .ThenInclude(d => d.MedicalRecord)
                        .ThenInclude(r => r.Department)
                .Include(f => f.MedicalDocuments)
                    .ThenInclude(d => d.MedicalRecord)
                        .ThenInclude(r => r.PaymentConfirmedByUser)
                            .ThenInclude(u => u.Person)
                .FirstOrDefaultAsync(f => f.Id == id);
            string ward = null;
            string province = null;
            if (!string.IsNullOrWhiteSpace(file.Patient.Person.Address))
            {
                var addressParts = file.Patient.Person.Address.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                if (addressParts.Length >= 2)
                {
                    ward = addressParts[0].Trim();
                    province = addressParts[1].Trim();
                }
                else if (addressParts.Length == 1)
                {
                    province = addressParts[0].Trim();
                }
            }

            response = new MedicalFileResponse
            {
                Id = file.Id,
                Status = file.Status ?? "Chưa xác định",
                MedicalCategoryName = file.FileCategory?.Name,
                Year = file.Year,
                CreatedAt = file.CreatedAt,
                CreatedByName = file.CreatedByNavigation?.Person?.FullName ?? "N/A",

                CurrentPatient = new PatientResponse
                {
                    Id = file.Patient.Id,
                    FullName = file.Patient.Person.FullName,
                    DateOfBirth = file.Patient.Person.DateOfBirth,
                    Gender = file.Patient.Person.Gender.HasValue
                        ? file.Patient.Person.Gender.Value ? "Nam" : "Nữ"
                        : "Không xác định",
                    Email = file.Patient.Person.User?.Email,
                    PhoneNumber = file.Patient.Person.PhoneNumber,
                    InsuranceNumber = file.Patient.InsuranceNumber,
                    AvatarPath = file.Patient.Person.AvatarPath,
                    Ward = ward,
                    Province = province,
                },

                DocumentTypes = file.MedicalDocuments
                   .Where(d => d.DocumentType != null)
                   .GroupBy(d => d.DocumentType)
                   .Select(g => new DocumentTypeDetailResponse
                   {
                       Id = g.Key.Id,
                       Name = g.Key.Name,
                       MaxSignatures = g.Key.MaxSignatures,

                       MedicalDocuments = g.Select(d => new MedicalDocumentDetailResponse
                       {
                           DocumentId = d.Id,
                           DocumentTypeName = d.DocumentType.Name,
                           Status = d.Status ?? "Chưa xác định",
                           CreatedAt = d.CreatedAt ?? DateTime.Now,
                           Name = d.Name,
                           MedicalRecords = d.MedicalRecord == null
                               ? new List<MedicalRecordResponse>()
                               : new List<MedicalRecordResponse>
                               {
                                    new MedicalRecordResponse
                                    {
                                        Id = d.MedicalRecord.Id,
                                        DoctorName = d.MedicalRecord.Doctor?.User?.Person?.FullName,
                                        DepartmentName = d.MedicalRecord.Department?.DepartmentName,
                                        Symptoms = d.MedicalRecord.Symptoms,
                                        Diagnosis = d.MedicalRecord.Diagnosis,
                                        ClinicalNotes = d.MedicalRecord.ClinicalNotes,
                                        TreatmentStatus = d.MedicalRecord.TreatmentStatus,
                                        RecordStatus = d.MedicalRecord.RecordStatus,
                                        VisitAt = d.MedicalRecord.VisitAt,
                                        CreatedAt = d.MedicalRecord.CreatedAt,
                                        UpdatedAt = d.MedicalRecord.UpdatedAt,
                                        PaymentStatus = d.MedicalRecord.PaymentStatus,
                                        PaidAt = d.MedicalRecord.PaidAt,
                                        PaymentConfirmedByName = d.MedicalRecord.PaymentConfirmedByUser?.Person?.FullName,

                                        ClinicalVitals = d.MedicalRecord.ClinicalVital != null
                                            ? new ClinicalVitalResponse
                                            {
                                                BloodPressure = d.MedicalRecord.ClinicalVital.BloodPressure,
                                                HeartRate = d.MedicalRecord.ClinicalVital.HeartRate,
                                                Temperature = (float?)d.MedicalRecord.ClinicalVital.Temperature,
                                                SpO2 = d.MedicalRecord.ClinicalVital.SpO2,
                                                Weight = (float?)d.MedicalRecord.ClinicalVital.Weight,
                                                Height = d.MedicalRecord.ClinicalVital.Height,
                                                RecordedAt = d.MedicalRecord.ClinicalVital.RecordedAt
                                            }
                                            : null,

                                        LabResults = d.MedicalRecord.LabResults != null && d.MedicalRecord.LabResults.Any()
                                            ? d.MedicalRecord.LabResults
                                                .Select(l => new LabResultResponse
                                                {
                                                    TestName = l.TestName,
                                                    ResultValue = l.ResultValue,
                                                    Unit = l.Unit,
                                                    ReferenceRange = l.ReferenceRange,
                                                    Evaluation = l.Evaluation
                                                })
                                                .ToList()
                                            : new List<LabResultResponse>()
                                    }
                                 }
                       }).ToList()
                   }).ToList()
            };

            return response;
        }
        public async Task<BaseResponse<MedicalFileResponse>> CreateNewMedicalFile(CreateMedicalFileRequest request)
        {
            var response = new BaseResponse<MedicalFileResponse>();
            List<string> validationError = Validator.ValidateMedicalFile(request);
            if (validationError.Count > 0)
            {
                response.IsSuccess = false;
                response.Message = string.Join("; ", validationError);
                response.data = null;
                return response;
            }
            //check bệnh nhân có tồn tại
            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == request.PatientId);

            if (!patientExists)
            {
                return new BaseResponse<MedicalFileResponse>
                {
                    IsSuccess = false,
                    Message = "Bệnh nhân không tồn tại trong hệ thống",
                    data = null
                };
            }
            //medicalCategoryId có tồn tại
            var category = await _context.FileCategories
                .FirstOrDefaultAsync(fc => fc.Id == request.FileCategoryId);

            if (category == null)
            {
                return new BaseResponse<MedicalFileResponse>
                {
                    IsSuccess = false,
                    Message = "Loại hồ sơ không tồn tại",
                    data = null
                };
            }
            MedicalFile info = new MedicalFile
            {
                Id = GenerateMedicalFileId(),
                PatientId = request.PatientId,
                FileCategoryId = request.FileCategoryId,
                Year = request.Year,
                Status = "Hồ sơ chờ",
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
            };
            
            var patient = await _context.Patients
                .Include(p => p.Person)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == request.PatientId);
            
            var createdByUser = await _context.Users
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.Id == request.CreatedBy);
            string ward = null;
            string province = null;
            if (!string.IsNullOrWhiteSpace(patient.Person.Address))
            {
                var addressParts = patient.Person.Address.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                if (addressParts.Length >= 2)
                {
                    ward = addressParts[0].Trim();
                    province = addressParts[1].Trim();
                }
                else if (addressParts.Length == 1)
                {
                    province = addressParts[0].Trim();
                }
            }
            _context.MedicalFiles.Add(info);
            await _context.SaveChangesAsync();
            MedicalFileResponse data = await GetMedicalFile(info.Id);
            response.IsSuccess = true;
            response.Message = "Tạo mới hồ sơ bệnh án thành công";
            response.data = data;
            return response;
        }
        
        public async Task<BaseResponse<MedicalFileResponse>> GetMedicalFileId(string id)
        {
            var response = new BaseResponse<MedicalFileResponse>();

            // Validate input
            if (string.IsNullOrWhiteSpace(id))
            {
                response.IsSuccess = false;
                response.Message = "ID hồ sơ bệnh án không hợp lệ";
                return response;
            }
            response.IsSuccess = true;
            response.Message = "Lấy hồ sơ bệnh án thành công";
            response.data = await GetMedicalFile(id);
            return response;
        }

        public async Task<BaseResponse<List<MedicalFilePagingResponse>>> GetMedicalFilesPage(GetMedicalFilePagingRequest request)
        {
            var response = new BaseResponse<List<MedicalFilePagingResponse>>();
            try
            {
                var query = _context.MedicalFiles
                    .Include(mf => mf.FileCategory)
                    .Include(mf => mf.Patient)
                        .ThenInclude(p => p.Person)
                    .Include(mf => mf.CreatedByNavigation)
                        .ThenInclude(u => u.Person)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    var keyword = request.Keyword.Trim();

                    query = query.Where(p =>
                        p.PatientId.Contains(keyword) ||
                        p.Patient.Person.FullName.Contains(keyword) ||
                        p.FileCategory != null && p.FileCategory.Name.Contains(keyword) ||
                        p.CreatedByNavigation != null && p.CreatedByNavigation.Person.FullName.Contains(keyword)
                    );
                }
                int totalSkip = (request.PageIndex - 1) * request.PageSize;

                var medicalFiles = await query
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip(totalSkip)
                    .Take(request.PageSize)
                    .ToListAsync();

                var result = medicalFiles.Select((p, i) =>
                {
                    return new MedicalFilePagingResponse
                    {
                        STT = totalSkip + i + 1,
                        Id = p.Id,
                        Status = p.Status ?? "Chưa xác định",
                        PatientId = p.PatientId,
                        PatientName = p.Patient.Person.FullName,
                        MedicalCategoryName = p.FileCategory?.Name,
                        Year = p.Year,
                        Gender = p.Patient.Person.Gender.HasValue
                            ? p.Patient.Person.Gender.Value ? "Nam" : "Nữ"
                            : "Không xác định",
                        CreatedAt = p.CreatedAt,
                        CreatedByName = p.CreatedByNavigation?.Person?.FullName ?? "N/A",
                    };
                })
                .ToList();

                // Apply additional filters
                if (request.CreatedAt.HasValue)
                {
                    result = result.Where(x => x.CreatedAt.HasValue &&
                        x.CreatedAt.Value.Date == request.CreatedAt.Value.Date).ToList();
                }

                if (!string.IsNullOrWhiteSpace(request.Status))
                {
                    result = result.Where(x => x.Status != null &&
                        x.Status.Contains(request.Status, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (request.YearOfFile.HasValue)
                {
                    result = result.Where(x => x.Year == request.YearOfFile.Value).ToList();
                }

                response.IsSuccess = true;
                response.Message = "Lấy dữ liệu Medical File thành công";
                response.data = result;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Lỗi lấy dữ liệu Medical File: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<MedicalFileResponse>> UpdateExistMedicalFile(string id, UpdateMedicalFileRequest request)
        {
            var response = new BaseResponse<MedicalFileResponse>();
            var file = await _context.MedicalFiles.FirstOrDefaultAsync(mf => mf.Id == id);
            if (file == null)
            {
                response.IsSuccess = false;
                response.Message = "Medical File không tồn tại";
                response.data = null;
                return response;
            }
            file.Status = request.Status ?? file.Status;
            _context.MedicalFiles.Update(file);
            await _context.SaveChangesAsync();
            response.IsSuccess = true;
            response.Message = "Cập nhật dữ liệu Medical File thành công";
            response.data = await GetMedicalFile(id);
            return response;
        }

        public async Task<BaseResponse<MedicalFileResponse>> DeleteExistMedicalFile(string id)
        {
            var response = new BaseResponse<MedicalFileResponse>();
            var file = await _context.MedicalFiles.FirstOrDefaultAsync(mf => mf.Id == id);
            _context.MedicalFiles.Update(file);
            await _context.SaveChangesAsync();
            response.IsSuccess = true;
            response.Message = "Không thể xóa dữ liệu Medical File, tất cả dữ liệu nên được lưu trữ";
            response.data = await GetMedicalFile(id);
            return response;
        }
    }
}
