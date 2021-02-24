using MediatR;

namespace Application.Common.Interfaces
{
    public interface IHangfireOverMediator
    {
        void Enqueue(string jobName, IRequest request);
    }
}