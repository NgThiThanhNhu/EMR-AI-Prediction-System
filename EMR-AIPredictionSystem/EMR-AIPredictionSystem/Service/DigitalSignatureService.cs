using EMR_AIPredictionSystem.Controllers;
using EMR_AIPredictionSystem.Data;
using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.DigitalSignature;
using Microsoft.EntityFrameworkCore;

namespace EMR_AIPredictionSystem.Service
{
    public class DigitalSignatureService : IDigitalSignatureService
    {
        private readonly ApplicationDbContext _context;

        public DigitalSignatureService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<List<GetAllDigitalSignatureResponse>>> GetDigitalSignatures()
        {
            var response = new BaseResponse<List<GetAllDigitalSignatureResponse>>();
            var query = _context.DigitalSignatures
                .Include(doc => doc.Document)
                .Include(doc => doc.SignedByNavigation)
                .ThenInclude(p => p.Person)
                .AsQueryable();
            if(!query.Any())
            {
                return new BaseResponse<List<GetAllDigitalSignatureResponse>>
                {
                    IsSuccess = false,
                    Message = "Không có chữ ký nào được ký ở các phiếu",
                    data = null
                };
            }
            return new BaseResponse<List<GetAllDigitalSignatureResponse>>
            {
                IsSuccess = true,
                Message = "Lấy chữ kí ở các phiếu thành công",
                data = await query.Select(ds => new GetAllDigitalSignatureResponse
                {
                    Id = ds.Id,
                    DocumentId = ds.DocumentId,
                    SignedBy = ds.SignedBy,
                    SignedByName = ds.SignedByNavigation.Person.FullName,
                    SignedAt = ds.SignedAt,
                    SignaturePath = ds.SignaturePath,
                    SignOrder = ds.SignOrder
                }).ToListAsync()
            };
        }
    }
}
