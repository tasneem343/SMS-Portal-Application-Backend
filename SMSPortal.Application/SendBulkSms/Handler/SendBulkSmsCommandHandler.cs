using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SMSPortal.Application.Interfaces.Repositories.SentMessages;
using SMSPortal.Application.SendBulkSms.Command;
using SMSPortal.Application.Services.SmsService;
using SMSPortal.Domain.Enitites.SentMessages;

namespace SMSPortal.Application.SendBulkSms.Handler
{
    class SendBulkSmsCommandHandler : IRequestHandler<SendBulkSmsCommand, List<int>>
    {
        private readonly ISmsService _smsService;
        private readonly ISentMessageRepo sentMessageRepo;

        public SendBulkSmsCommandHandler(ISmsService smsService, ISentMessageRepo sentMessageRepo)
        {
            _smsService = smsService;
            this.sentMessageRepo = sentMessageRepo;
        }

        public async Task<List<int>> Handle(SendBulkSmsCommand request, CancellationToken cancellationToken)
        {
            var messageIds = new List<int>();

            using var reader = new StreamReader(request.CsvFile.OpenReadStream(), Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                var phoneNumber = (await reader.ReadLineAsync())?.Trim();

                if (IsValidPhoneNumber(phoneNumber))
                {
                    var result = await _smsService.Send(phoneNumber, request.MessageContent);

                    var message = new SentMessage
                    {
                        PhoneNumber = phoneNumber,
                        MessageContent = request.MessageContent,
                        SenderUserId = request.SenderUserId,
                        SentAt = DateTime.UtcNow,
                        IsSuccessful = result.ErrorCode == null,

                    };

                   await sentMessageRepo.AddAsync(message);
                    

                    messageIds.Add(message.Id);
                }
            }

            return messageIds;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber)
                && phoneNumber.StartsWith("+")
                && phoneNumber.Length >= 11
                && phoneNumber.Skip(1).All(char.IsDigit); 
        }
    }
    
    
}
