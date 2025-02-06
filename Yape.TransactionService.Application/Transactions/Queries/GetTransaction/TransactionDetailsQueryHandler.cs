using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yape.TransactionService.Application.Common.Interfaces;

namespace Yape.TransactionService.Application.Transactions.Queries.GetTransaction
{
    //internal class TransactionDetailsQueryHandler : IRequestHandler<TransactionDetailsQuery, ErrorOr<TransactionDetailsResponse>>
    //{
    //    private readonly ITransactionRepository _transactionRepository;
    //    public TransactionDetailsQueryHandler(ITransactionRepository transactionRepository)
    //    {
    //        _transactionRepository = transactionRepository;
    //    }

    //    public async Task<ErrorOr<TransactionDetailsResponse>> Handle(TransactionDetailsQuery request, CancellationToken cancellationToken)
    //    {
    //        var resut = await _transactionRepository.GetTransactionAsync(request.transactionId);

    //        if (resut == null)             
    //        { 
    //          //return resut.ToErrorOr<string>("Transaction not found");
    //        }

    //        var response = new TransactionDetailsResponse(request.transactionId);

    //        return response;
    //    }
    //}
}
