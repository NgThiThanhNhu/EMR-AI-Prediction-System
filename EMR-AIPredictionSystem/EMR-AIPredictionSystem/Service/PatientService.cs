using Azure;
using Azure.Core;
using EMR_AIPredictionSystem.Data;
using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Request.User;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.Authentication;
using EMR_AIPredictionSystem.Model.Response.Patient;
using EMR_AIPredictionSystem.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using EMR_AIPredictionSystem.Utils;
using Validator = EMR_AIPredictionSystem.Utils.Validator;
namespace EMR_AIPredictionSystem.Service
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<List<PatientPagingResponse>>> GetPatientsPage(GetPatientPagingRequest request)
        {
            var response = new BaseResponse<List<PatientPagingResponse>>();
            var query = _context.Patients.Include(p => p.Person).Where(p => p.IsDeleted == false);
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(p =>
                    p.Id.Contains(request.Keyword) ||
                    p.Person.FullName.Contains(request.Keyword) ||
                    p.Person.PhoneNumber.Contains(request.Keyword)
                );
            }
            int totalSkip = (request.PageIndex - 1) * request.PageSize;

            var patients = await query
                .OrderByDescending(p => p.Person.CreatedAt)
                .Skip(totalSkip)
                .Take(request.PageSize)
                .ToListAsync();
            var result = patients.Select((p, i) =>
            {
                string ward = "";
                string province = "";

                if (!string.IsNullOrWhiteSpace(p.Person.Address))
                {
                    var parts = p.Person.Address.Split('-', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        ward = parts[0].Trim();
                        province = parts[1].Trim();
                    }
                }

                return new PatientPagingResponse
                {
                    STT = totalSkip + i + 1,
                    Id = p.Id,
                    FullName = p.Person.FullName,
                    Ward = ward,
                    Province = province,
                    Gender = p.Person.Gender == true ? "Nam" : "Nữ",
                    DateOfBirth = DateOnly.FromDateTime((DateTime)p.Person.DateOfBirth),
                    PhoneNumber = p.Person.PhoneNumber
                };
            })
            .ToList();

            if (!string.IsNullOrWhiteSpace(request.Province))
                result = result.Where(x => x.Province.Contains(request.Province)).ToList();

            if (!string.IsNullOrWhiteSpace(request.Ward))
                result = result.Where(x => x.Ward.Contains(request.Ward)).ToList();

            if (request.YearOfBirth.HasValue)
            {
                result = result
                    .Where(x => x.DateOfBirth?.Year == request.YearOfBirth.Value)
                    .ToList();
            }
            response.IsSuccess = true;
            response.data = result;
            return response;
        }
        private string GeneratePatientId()
        {
            string prefix = "BN-";
            int maxIdNumber = 0;
            var existingIds = _context.Patients
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
        public async Task<BaseResponse<PatientResponse>> CreateNewPatient(CreatePatientRequest request)
        {
            BaseResponse<PatientResponse> response = new BaseResponse<PatientResponse>();
            List<string> validationError = Validator.ValidateCreatePatient(request);
            if (validationError.Count > 0)
            {
                response.IsSuccess = false;
                response.Message = string.Join("; ", validationError);
                response.data = null;
                return response;
            }
            var person = new Person
            {
                FullName = request.FullName,
                Gender = request.Gender == "Nữ" ? false : true,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                AvatarPath = request.AvatarPath,
                Address = $"{request.Ward} - {request.Province}",
                CreatedAt = DateTime.Now
            };

            var patient = new Patient
            {
                Id = GeneratePatientId(),
                InsuranceNumber = request.InsuranceNumber,
                CreateDate = DateTime.Now,
                Person = person
            };

            _context.Patients.Add(patient);
            //Cần bắt lỗi để trả về false
            await _context.SaveChangesAsync();
            PatientResponse patientResponse = new PatientResponse
            {
                Id = patient.Id,
                FullName = person.FullName,
                PhoneNumber = person.PhoneNumber,
                InsuranceNumber = request.InsuranceNumber,
                Gender = person.Gender == true ? "Name" : "Nữ",
                DateOfBirth = person.DateOfBirth,
                Province = request.Province,
                Ward = request.Ward,
                AvatarPath = person.AvatarPath
            };
            return new BaseResponse<PatientResponse>
            {
                IsSuccess = true,
                Message = "Tạo bệnh nhân thành công",
                data = patientResponse
            };
        }

        public async Task<BaseResponse<PatientResponse>> UpdateExistPatient(string id, UpdatePatientRequest request)
        {
            BaseResponse<PatientResponse> response = new BaseResponse<PatientResponse>();
            var patient = _context.Patients
                .Include(p => p.Person).ThenInclude(p => p.User)
                .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
            //chưa có check email đã tồn tại hay chưa(nhớ làm nếu có user)
            if (patient != null) {
                if (patient.Person.User != null && !string.IsNullOrWhiteSpace(request.Email))
                {
                    var existingUser = _context.Users
                        .FirstOrDefault(u => u.Email == request.Email && u.Id != patient.Person.User.Id);
                    if (existingUser != null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Email đã được sử dụng bởi người dùng khác";
                        response.data = null;
                        return response;
                    }
                }
            }
            //check số điện thoại đã tồn tại hay chưa
            if( patient != null)
            {
                var existingPhoneNumber = _context.Persons
                    .FirstOrDefault(p => p.PhoneNumber == request.PhoneNumber && p.Id != patient.Person.Id);
                if (existingPhoneNumber != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Số điện thoại đã được sử dụng bởi người khác";
                    response.data = null;
                    return response;
                }
            }
            List<string> validationError = Validator.ValidateUpdatePatient(request);
            if (patient == null || validationError.Count > 0)
            {
                response.IsSuccess = false;
                response.Message = "Bệnh nhân không tồn tại";
                response.data = null;
                return response;
            }
            if (!string.IsNullOrWhiteSpace(request.FullName))
                patient.Person.FullName = request.FullName;
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                patient.Person.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(request.Ward) && !string.IsNullOrWhiteSpace(request.Province))
                patient.Person.Address = $"{request.Ward} - {request.Province}";
            if (!string.IsNullOrWhiteSpace(request.AvatarPath))
                patient.Person.AvatarPath = request.AvatarPath;
            if(patient.Person.User != null && !string.IsNullOrWhiteSpace(request.Email))
                patient.Person.User.Email = request.Email;
            patient.UpdateDate = DateTime.Now;
            if(patient.Person.User != null) patient.Person.User.UpdateDate = DateTime.Now;
             _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            PatientResponse patientResponse = new PatientResponse
            {
                Id = patient.Id,
                FullName = patient.Person.FullName,
                PhoneNumber = patient.Person.PhoneNumber,
                InsuranceNumber = patient.InsuranceNumber,
                AvatarPath = patient.Person.AvatarPath,
                DateOfBirth = patient.Person.DateOfBirth,
                Gender = patient.Person.Gender == true ? "Nam" : "Nữ",
                Province = patient.Person.Address?.Split(" - ").LastOrDefault(),
                Ward = patient.Person.Address?.Split(" - ").FirstOrDefault(),
                Email = patient.Person.User != null ? patient.Person.User.Email : null
            };
            return new BaseResponse<PatientResponse>
            {
                IsSuccess = true,
                Message = "Cập nhật bệnh nhân thành công",
                data = patientResponse
            };
        }

        public async Task<BaseResponse<PatientResponse>> DeleteExistPatient(string id)
        {
            //xóa mềm isDelete ở bảng patient và user nếu có
            var patient = _context.Patients
               .Include(p => p.Person).ThenInclude(p => p.User)
               .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
            if (patient == null)
            {
                return (new BaseResponse<PatientResponse>
                {
                    IsSuccess = false,
                    Message = "Bệnh nhân không tồn tại",
                    data = null
                });
            }
            patient.IsDeleted = true;
            patient.UpdateDate = DateTime.Now;
            patient.DeleteDate = DateTime.Now;
            if (patient.Person.User != null)
            {
                patient.Person.User.IsDeleted = true;
                patient.Person.User.UpdateDate = DateTime.Now;
                patient.Person.User.DeleteDate = DateTime.Now;
                _context.Users.Update(patient.Person.User);
            }
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return new BaseResponse<PatientResponse>
            {
                IsSuccess = true,
                Message = "Xóa bệnh nhân thành công",
                data = null
            };
        }
    }
}
