
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yape.TransactionService.Contracts.Transactions;
using Yape.TransactionService.Contracts.Transactions.Commands;

namespace Yape.TransactionService.Api.Controllers
{
    [Route("[controller]")]
    public class FinancialTransactionController : ApiController
    {        
        private readonly IMediator _mediator;

        public FinancialTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateTransactionRequest request)
        {
            var command = new CreateTransactionCommand(request.SourceAccountId, request.TargetAccountId, request.TransferTypeId, request.Value);
            var result =  await _mediator.Send(command);
            
            return result.IsSuccess ? Ok(result) : BadRequest(result);            
        }      
    }
}
