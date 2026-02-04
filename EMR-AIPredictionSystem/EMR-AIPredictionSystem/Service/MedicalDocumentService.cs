using Azure;
using Azure.Core;
using EMR_AIPredictionSystem.Data;
using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Request.MedicalDocument;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.DigitalSignature;
using EMR_AIPredictionSystem.Model.Response.MedicalDocument;
using EMR_AIPredictionSystem.Model.Response.MedicalFiles;
using EMR_AIPredictionSystem.Model.Response.MedicalRecord;
using EMR_AIPredictionSystem.Model.Response.Patient;
using Microsoft.EntityFrameworkCore;

namespace EMR_AIPredictionSystem.Service
{
    public class MedicalDocumentService : IMedicalDocumentService
    {
        private readonly ApplicationDbContext _context;
        
        public MedicalDocumentService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<MedicalDocumentResponse> GetMedicalFile(string id)
        {
            var md = await _context.MedicalDocuments
                 .Include(x => x.DocumentType)
                 .Include(x => x.MedicalFile)
                     .ThenInclude(mf => mf.FileCategory)
                 .Include(x => x.MedicalFile)
                     .ThenInclude(mf => mf.Patient)
                         .ThenInclude(p => p.Person)
                             .ThenInclude(per => per.User)
                 .Include(x => x.MedicalRecord)
                     .ThenInclude(mr => mr.ClinicalVital)
                 .Include(x => x.MedicalRecord)
                     .ThenInclude(mr => mr.LabResults)
                 .Include(x => x.DigitalSignatures)
                     .ThenInclude(ds => ds.SignedByNavigation)
                         .ThenInclude(u => u.Person)
                 .FirstOrDefaultAsync(x => x.Id == id);

            // Parse address
            string ward = null;
            string province = null;

            var address = md.MedicalFile?.Patient?.Person?.Address;
            if (!string.IsNullOrWhiteSpace(address))
            {
                var parts = address.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    ward = parts[0].Trim();
                    province = parts[1].Trim();
                }
                else
                {
                    province = parts[0].Trim();
                }
            }

            return new MedicalDocumentResponse
            {
                Name = md.Name ?? "Chưa đặt tên",
                MedicalFileName = md.MedicalFile?.FileCategory?.Name,
                SignaturedQuantity = md.DigitalSignatures?.Count ?? 0,
                Status = md.Status ?? "DRAFT",
                CreatedAt = md.CreatedAt,

                Patient = md.MedicalFile?.Patient == null ? null : new PatientResponse
                {
                    Id = md.MedicalFile.Patient.Id,
                    FullName = md.MedicalFile.Patient.Person?.FullName,
                    DateOfBirth = md.MedicalFile.Patient.Person?.DateOfBirth,
                    Gender = md.MedicalFile.Patient.Person?.Gender == true ? "Nam" : "Nữ",
                    Email = md.MedicalFile.Patient.Person?.User?.Email,
                    PhoneNumber = md.MedicalFile.Patient.Person?.PhoneNumber,
                    Province = province,
                    Ward = ward,
                    InsuranceNumber = md.MedicalFile.Patient.InsuranceNumber,
                    AvatarPath = md.MedicalFile.Patient.Person?.AvatarPath
                },

                DigitalSignatures = md.DigitalSignatures?
                    .OrderBy(x => x.SignOrder)
                    .Select(ds => new DigitalSignatureResponse
                    {
                        Id = ds.Id,
                        SignedBy = ds.SignedBy,
                        SignedByName = ds.SignedByNavigation?.Person?.FullName,
                        SignedAt = ds.SignedAt,
                        SignaturePath = ds.SignaturePath,
                        SignOrder = ds.SignOrder
                    }).ToList()
                    ?? new List<DigitalSignatureResponse>(),

                MedicalRecords = md.MedicalRecord == null ? null : new MedicalRecordResponse
                {
                    Id = md.MedicalRecord.Id,
                    Symptoms = md.MedicalRecord.Symptoms,
                    Diagnosis = md.MedicalRecord.Diagnosis,
                    ClinicalNotes = md.MedicalRecord.ClinicalNotes,
                    TreatmentStatus = md.MedicalRecord.TreatmentStatus,
                    RecordStatus = md.MedicalRecord.RecordStatus,
                    VisitAt = md.MedicalRecord.VisitAt,
                    CreatedAt = md.MedicalRecord.CreatedAt,
                    UpdatedAt = md.MedicalRecord.UpdatedAt,

                    ClinicalVitals = md.MedicalRecord.ClinicalVital == null ? null : new ClinicalVitalResponse
                    {
                        BloodPressure = md.MedicalRecord.ClinicalVital.BloodPressure,
                        HeartRate = md.MedicalRecord.ClinicalVital.HeartRate,
                        Temperature = (float?)md.MedicalRecord.ClinicalVital.Temperature,
                        SpO2 = md.MedicalRecord.ClinicalVital.SpO2,
                        Weight = (float?)md.MedicalRecord.ClinicalVital.Weight,
                        Height = md.MedicalRecord.ClinicalVital.Height,
                        RecordedAt = md.MedicalRecord.ClinicalVital.RecordedAt
                    },

                    LabResults = md.MedicalRecord.LabResults?
                        .Select(lr => new LabResultResponse
                        {
                            TestName = lr.TestName,
                            ResultValue = lr.ResultValue,
                            Unit = lr.Unit,
                            ReferenceRange = lr.ReferenceRange,
                            Evaluation = lr.Evaluation
                        }).ToList()
                        ?? new List<LabResultResponse>()
                }
            };
        }

        public async Task<BaseResponse<MedicalDocumentResponse>> GetMedicalDocumentById(string id)
        {
            var response = new BaseResponse<MedicalDocumentResponse>();
            //check id có trong document ở db không
            
            var md = await _context.MedicalDocuments
                .Include(x => x.DocumentType)
                .Include(x => x.MedicalFile)
                    .ThenInclude(mf => mf.FileCategory)
                .Include(x => x.MedicalFile)
                    .ThenInclude(mf => mf.Patient)
                        .ThenInclude(p => p.Person)
                            .ThenInclude(per => per.User)
                .Include(x => x.MedicalRecord)
                    .ThenInclude(mr => mr.ClinicalVital)
                .Include(x => x.MedicalRecord)
                    .ThenInclude(mr => mr.LabResults)
                .Include(x => x.DigitalSignatures)
                    .ThenInclude(ds => ds.SignedByNavigation)
                        .ThenInclude(u => u.Person)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (md == null)
            {
                response.IsSuccess = false;
                response.Message = "Không tìm thấy Medical Document";
                return response;
            }

            // Parse address
            string ward = null;
            string province = null;

            var address = md.MedicalFile?.Patient?.Person?.Address;
            if (!string.IsNullOrWhiteSpace(address))
            {
                var parts = address.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    ward = parts[0].Trim();
                    province = parts[1].Trim();
                }
                else
                {
                    province = parts[0].Trim();
                }
            }

            response.data = new MedicalDocumentResponse
            {
                Name = md.Name ?? "Chưa đặt tên",
                MedicalFileName = md.MedicalFile?.FileCategory?.Name,
                SignaturedQuantity = md.DigitalSignatures?.Count ?? 0,
                Status = md.Status ?? "DRAFT",
                CreatedAt = md.CreatedAt,

                Patient = md.MedicalFile?.Patient == null ? null : new PatientResponse
                {
                    Id = md.MedicalFile.Patient.Id,
                    FullName = md.MedicalFile.Patient.Person?.FullName,
                    DateOfBirth = md.MedicalFile.Patient.Person?.DateOfBirth,
                    Gender = md.MedicalFile.Patient.Person?.Gender == true ? "Nam" : "Nữ",
                    Email = md.MedicalFile.Patient.Person?.User?.Email,
                    PhoneNumber = md.MedicalFile.Patient.Person?.PhoneNumber,
                    Province = province,
                    Ward = ward,
                    InsuranceNumber = md.MedicalFile.Patient.InsuranceNumber,
                    AvatarPath = md.MedicalFile.Patient.Person?.AvatarPath
                },

                DigitalSignatures = md.DigitalSignatures?
                    .OrderBy(x => x.SignOrder)
                    .Select(ds => new DigitalSignatureResponse
                    {
                        Id = ds.Id,
                        SignedBy = ds.SignedBy,
                        SignedByName = ds.SignedByNavigation?.Person?.FullName,
                        SignedAt = ds.SignedAt,
                        SignaturePath = ds.SignaturePath,
                        SignOrder = ds.SignOrder
                    }).ToList()
                    ?? new List<DigitalSignatureResponse>(),

                MedicalRecords = md.MedicalRecord == null ? null : new MedicalRecordResponse
                {
                    Id = md.MedicalRecord.Id,
                    Symptoms = md.MedicalRecord.Symptoms,
                    Diagnosis = md.MedicalRecord.Diagnosis,
                    ClinicalNotes = md.MedicalRecord.ClinicalNotes,
                    TreatmentStatus = md.MedicalRecord.TreatmentStatus,
                    RecordStatus = md.MedicalRecord.RecordStatus,
                    VisitAt = md.MedicalRecord.VisitAt,
                    CreatedAt = md.MedicalRecord.CreatedAt,
                    UpdatedAt = md.MedicalRecord.UpdatedAt,

                    ClinicalVitals = md.MedicalRecord.ClinicalVital == null ? null : new ClinicalVitalResponse
                    {
                        BloodPressure = md.MedicalRecord.ClinicalVital.BloodPressure,
                        HeartRate = md.MedicalRecord.ClinicalVital.HeartRate,
                        Temperature = (float?)md.MedicalRecord.ClinicalVital.Temperature,
                        SpO2 = md.MedicalRecord.ClinicalVital.SpO2,
                        Weight = (float?)md.MedicalRecord.ClinicalVital.Weight,
                        Height = md.MedicalRecord.ClinicalVital.Height,
                        RecordedAt = md.MedicalRecord.ClinicalVital.RecordedAt
                    },

                    LabResults = md.MedicalRecord.LabResults?
                        .Select(lr => new LabResultResponse
                        {
                            TestName = lr.TestName,
                            ResultValue = lr.ResultValue,
                            Unit = lr.Unit,
                            ReferenceRange = lr.ReferenceRange,
                            Evaluation = lr.Evaluation
                        }).ToList()
                        ?? new List<LabResultResponse>()
                }
            };

            response.IsSuccess = true;
            response.Message = "Lấy Medical Document theo ID thành công";
            return response;
        }


        public async Task<BaseResponse<List<MedicalDocumentPagingResponse>>> GetMedicalDocumentPage(MedicalDocumentPagingRequest request)
        {
            var response = new BaseResponse<List<MedicalDocumentPagingResponse>>();

            try
            {
                // Build query with all necessary includes
                var query = _context.MedicalDocuments
                    .Include(md => md.DocumentType)
                    .Include(md => md.MedicalFile)
                        .ThenInclude(mf => mf.FileCategory)
                    .Include(md => md.MedicalFile)
                        .ThenInclude(mf => mf.Patient)
                            .ThenInclude(p => p.Person)
                                .ThenInclude(person => person.User)
                    .Include(md => md.MedicalRecord)
                        .ThenInclude(mr => mr.ClinicalVital)
                    .Include(md => md.MedicalRecord)
                        .ThenInclude(mr => mr.LabResults)
                    .Include(md => md.DigitalSignatures)
                        .ThenInclude(ds => ds.SignedByNavigation)
                            .ThenInclude(u => u.Person)
                    .AsQueryable();

                // Apply keyword search filter
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    var keyword = request.Keyword.Trim().ToLower();

                    query = query.Where(md =>
                        (md.Status != null && md.Status.ToLower().Contains(keyword)) ||
                        (md.Name != null && md.Name.ToLower().Contains(keyword)) ||
                        (md.DocumentType != null && md.DocumentType.Name.ToLower().Contains(keyword)) ||
                        (md.MedicalFile != null && 
                            md.MedicalFile.FileCategory != null && 
                            md.MedicalFile.FileCategory.Name.ToLower().Contains(keyword)) ||
                        (md.MedicalFile != null && 
                            md.MedicalFile.Patient != null && 
                            md.MedicalFile.Patient.Person != null && 
                            md.MedicalFile.Patient.Person.FullName.ToLower().Contains(keyword)) ||
                        md.DigitalSignatures.Any(ds => 
                            ds.SignedByNavigation != null && 
                            ds.SignedByNavigation.Person != null && 
                            ds.SignedByNavigation.Person.FullName.ToLower().Contains(keyword))
                    );
                }

                // Apply FileCategoryId filter
                if (!string.IsNullOrWhiteSpace(request.FileCategoryId))
                {
                    query = query.Where(md => 
                        md.MedicalFile != null && 
                        md.MedicalFile.FileCategoryId == request.FileCategoryId);
                }

                // Apply SignaturedBy filter
                if (request.SignaturedBy.HasValue)
                {
                    query = query.Where(md => 
                        md.DigitalSignatures.Any(ds => ds.SignedBy == request.SignaturedBy.Value));
                }

                // Apply CreatedAt filter
                if (request.CreatedAt.HasValue)
                {
                    var searchDate = request.CreatedAt.Value.Date;
                    query = query.Where(md => 
                        md.CreatedAt.HasValue && 
                        md.CreatedAt.Value.Date == searchDate);
                }

                // Calculate pagination
                int totalSkip = (request.PageIndex - 1) * request.PageSize;

                // Execute query with pagination
                var medicalDocuments = await query
                    .OrderByDescending(md => md.CreatedAt)
                    .Skip(totalSkip)
                    .Take(request.PageSize)
                    .ToListAsync();

                // Map to response
                var result = medicalDocuments.Select((md, index) =>
                {
                    // Parse address
                    string ward = null;
                    string province = null;
                    
                    if (md.MedicalFile?.Patient?.Person?.Address != null)
                    {
                        var addressParts = md.MedicalFile.Patient.Person.Address
                            .Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                        
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

                    return new MedicalDocumentPagingResponse
                    {
                        STT = totalSkip + index + 1,
                        DocumentId = md.Id,
                        Name = md.Name ?? "Chưa đặt tên",
                        MedicalFileName = md.MedicalFile?.FileCategory?.Name ?? "N/A",
                        SignaturedQuantity = md.DigitalSignatures?.Count ?? 0,
                        Status = md.Status ?? "DRAFT",
                        CreatedAt = md.CreatedAt,

                        Patient = md.MedicalFile?.Patient != null
                            ? new Model.Response.Patient.PatientResponse
                            {
                                Id = md.MedicalFile.Patient.Id,
                                FullName = md.MedicalFile.Patient.Person?.FullName ?? "N/A",
                                DateOfBirth = md.MedicalFile.Patient.Person?.DateOfBirth,
                                Gender = md.MedicalFile.Patient.Person?.Gender.HasValue == true
                                    ? (md.MedicalFile.Patient.Person.Gender.Value ? "Nam" : "Nữ")
                                    : "Không xác định",
                                Email = md.MedicalFile.Patient.Person?.User?.Email,
                                Province = province,
                                Ward = ward,
                                PhoneNumber = md.MedicalFile.Patient.Person?.PhoneNumber,
                                InsuranceNumber = md.MedicalFile.Patient.InsuranceNumber,
                                AvatarPath = md.MedicalFile.Patient.Person?.AvatarPath
                            }
                            : null,

                        DigitalSignatures = md.DigitalSignatures != null && md.DigitalSignatures.Any()
                            ? md.DigitalSignatures
                                .OrderBy(ds => ds.SignOrder)
                                .Select(ds => new Model.Response.DigitalSignature.DigitalSignatureResponse
                                {
                                    Id = ds.Id,
                                    SignedBy = ds.SignedBy,
                                    SignedByName = ds.SignedByNavigation?.Person?.FullName ?? "N/A",
                                    SignedAt = ds.SignedAt,
                                    SignaturePath = ds.SignaturePath,
                                    SignOrder = ds.SignOrder
                                })
                                .ToList()
                            : new List<Model.Response.DigitalSignature.DigitalSignatureResponse>(),

                        MedicalRecords = md.MedicalRecord != null
                            ? new MedicalRecordResponse
                            {
                                Id = md.MedicalRecord.Id,
                                DoctorName = md.MedicalRecord.Doctor?.User?.Person?.FullName,
                                DepartmentName = md.MedicalRecord.Department?.DepartmentName,
                                Symptoms = md.MedicalRecord.Symptoms,
                                Diagnosis = md.MedicalRecord.Diagnosis,
                                ClinicalNotes = md.MedicalRecord.ClinicalNotes,
                                TreatmentStatus = md.MedicalRecord.TreatmentStatus,
                                RecordStatus = md.MedicalRecord.RecordStatus,
                                VisitAt = md.MedicalRecord.VisitAt,
                                CreatedAt = md.MedicalRecord.CreatedAt,
                                UpdatedAt = md.MedicalRecord.UpdatedAt,
                                PaymentStatus = md.MedicalRecord.PaymentStatus,
                                PaidAt = md.MedicalRecord.PaidAt,
                                PaymentConfirmedByName = md.MedicalRecord.PaymentConfirmedByUser?.Person?.FullName,

                                ClinicalVitals = md.MedicalRecord.ClinicalVital != null
                                    ? new ClinicalVitalResponse
                                    {
                                        BloodPressure = md.MedicalRecord.ClinicalVital.BloodPressure,
                                        HeartRate = md.MedicalRecord.ClinicalVital.HeartRate,
                                        Temperature = md.MedicalRecord.ClinicalVital.Temperature.HasValue
                                            ? (float?)md.MedicalRecord.ClinicalVital.Temperature.Value
                                            : null,
                                        SpO2 = md.MedicalRecord.ClinicalVital.SpO2,
                                        Weight = md.MedicalRecord.ClinicalVital.Weight.HasValue
                                            ? (float?)md.MedicalRecord.ClinicalVital.Weight.Value
                                            : null,
                                        Height = md.MedicalRecord.ClinicalVital.Height,
                                        RecordedAt = md.MedicalRecord.ClinicalVital.RecordedAt
                                    }
                                    : null,

                                LabResults = md.MedicalRecord.LabResults != null && md.MedicalRecord.LabResults.Any()
                                    ? md.MedicalRecord.LabResults.Select(lr => new LabResultResponse
                                    {
                                        TestName = lr.TestName,
                                        ResultValue = lr.ResultValue,
                                        Unit = lr.Unit,
                                        ReferenceRange = lr.ReferenceRange,
                                        Evaluation = lr.Evaluation
                                    }).ToList()
                                    : new List<LabResultResponse>()
                            }
                            : null
                    };
                }).ToList();

                response.IsSuccess = true;
                response.Message = $"Lấy danh sách Medical Document thành công. Tổng số: {result.Count}";
                response.data = result;
                
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Lỗi khi lấy danh sách Medical Document: {ex.Message}";
                response.data = new List<MedicalDocumentPagingResponse>();
                
                return response;
            }
        }

        public async Task<BaseResponse<MedicalDocumentResponse>> UpdateExisMedicalDocument(string id, UpdateMedicalDocumentRequest request)
        {
            var response = new BaseResponse<MedicalDocumentResponse>();
            //check status hợp lệ enum
            var doc = await _context.MedicalDocuments.FirstOrDefaultAsync(mf => mf.Id == id);
            if (doc == null)
            {
                response.IsSuccess = false;
                response.Message = "Medical Document không tồn tại";
                response.data = null;
                return response;
            }
            doc.Status = request.Status ?? doc.Status;
            _context.MedicalDocuments.Update(doc);
            await _context.SaveChangesAsync();
            response.IsSuccess = true;
            response.Message = "Cập nhật dữ liệu Medical Document thành công";
            response.data = await GetMedicalFile(id);
            return response;
        }
    }
}
