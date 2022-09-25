namespace SecondService.Services.Implementation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Message;
    using Utils;

    /// <inheritdoc cref="IRequestHandler"/>
    public class RequestHandler : IRequestHandler
    {
        private readonly IServiceProvider _serviceProvider;

        private List<IProcessCalculateHandler> _processCalculateHandlers = null;
        private BlockingCollection<MessageDto> _blockingCollection = null;
        
        public RequestHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void StartCalculate(int countThread)
        {
            StopCalculate();
            
            _processCalculateHandlers = new List<IProcessCalculateHandler>(countThread);
            _blockingCollection = new BlockingCollection<MessageDto>();
            
            for (int i = 0; i < countThread; i++)
            {
                var processCalculateHandler = _serviceProvider.ResolveService<IProcessCalculateHandler>(); 
                _processCalculateHandlers.Add(processCalculateHandler);
                processCalculateHandler.StartProcess(_blockingCollection);
            } 
        }
        public void StopCalculate()
        {
            if (_processCalculateHandlers == null)
            {
                return;
            }
            
            foreach (var handler in _processCalculateHandlers)
            {
                handler.Stop();
            }
            _processCalculateHandlers.Clear();
            _processCalculateHandlers = null;
            _blockingCollection.CompleteAdding();
            _blockingCollection = null;
        }
        
        public void HandleMsg(MessageDto msg)
        {
            _blockingCollection.TryAdd(msg);
        }
    }
}