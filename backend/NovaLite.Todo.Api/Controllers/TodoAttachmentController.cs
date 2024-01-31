using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Novalite.Todo.Shared.DTOs;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;
using Novalite.Todo.Shared.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Shared.Repos.TodoListRepo;

namespace NovaLite.Todo.Api.Controllers
{
    [Route("api/attachments")]
    [ApiController]
    public class TodoAttachmentController : ControllerBase
    {
        private readonly BlobStorageService _blobService;
        private readonly IUnitOfWork _unitOfWork;

        public TodoAttachmentController(BlobStorageService blobService, IUnitOfWork unitOfWork)
        {
            _blobService = blobService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{todoListId}")]
        public async Task<ActionResult<IEnumerable<TodoAttachment>>> GetAllFromList(Guid todoListId)
        {
            var attachments = await _unitOfWork.TodoAttachmentRepository.GetAllFromListAsync(todoListId);
            return Ok(attachments);
        }

        [HttpGet("getSas/{blobName}/{todoListId}")]
        public async Task<ActionResult<SasResponse>> GetSasToken(string blobName, string todoListId)
        {
            TodoAttachment todo = new TodoAttachment()
            {
                FileName = blobName,
                TodoListId = Guid.Parse(todoListId)
            };
            var blob =  await _unitOfWork.TodoAttachmentRepository.CreateAsync(todo);
            await _unitOfWork.CompleteAsync();
            var blobSasPermissions = BlobSasPermissions.Write;
            var sasToken = _blobService.GenerateSasToken(blob.Id.ToString(), blobSasPermissions); // Adjust permissions as needed
            var response = new SasResponse()
            {
                Sas = sasToken,
                AttachmentId = blob.Id.ToString()
            };
            return Ok(response);
        }

        [HttpGet("getSas/{blobName}")]
        public async Task<ActionResult<SasResponse>> GetSasToken(string blobName)
        {
            var blob = await _unitOfWork.TodoAttachmentRepository.GetByIdAsync(Guid.Parse(blobName));
            var blobSasPermissions = BlobSasPermissions.Read;
            var sasToken = _blobService.GenerateSasToken(blob.Id.ToString(), blobSasPermissions); 
            var response = new SasResponse()
            {
                Sas = sasToken,
                AttachmentId = blob.Id.ToString()
            };
            return Ok(response);

        }
    }
}