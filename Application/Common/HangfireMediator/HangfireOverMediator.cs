using System.ComponentModel;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Hangfire;
using MediatR;

namespace Application.Common.HangfireMediator
{
    public class HangfireOverMediator : IHangfireOverMediator
    {
        private readonly IMediator _mediator;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireOverMediator(IMediator mediator,
            IBackgroundJobClient backgroundJobClient)
        {
            _mediator = mediator;
            _backgroundJobClient = backgroundJobClient;
        }

        public void Enqueue(string jobName, IRequest request)
        {
            _backgroundJobClient.Enqueue<HangfireOverMediator>(bridge => bridge.Send(jobName, request));
        }

        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
    }
}