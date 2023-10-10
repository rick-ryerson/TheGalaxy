using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace GalacticSenate.Library {
   public abstract class BasicServiceBase {
      protected readonly IUnitOfWork<DataContext> unitOfWork;
      protected readonly IEventBus eventBus;
      protected readonly ILogger logger;

      protected BasicServiceBase(IUnitOfWork<DataContext> unitOfWork, IEventBus eventBus, ILogger logger) {
         this.unitOfWork = unitOfWork;
         this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
         this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
      }
   }
}
