using System;
using System.Collections.Generic;

namespace GalacticSenate.Library {
   public enum StatusEnum
    {
        NotSet,
        Successful,
        Failed
    }
    public class ModelResponse<TModel, TRequest> : BasicResponse<TRequest>
    {
        public ModelResponse(DateTime startTime, TRequest request) : base(startTime, request)
        {

        }

        public List<TModel> Results { get; set; } = new List<TModel>();

        public new ModelResponse<TModel, TRequest> Finalize()
        {
            return (ModelResponse<TModel, TRequest>)base.Finalize();
        }
        new ModelResponse<TModel, TRequest> Finalize(DateTime finish)
        {
            return (ModelResponse<TModel, TRequest>)base.Finalize(finish);
        }
    }

    public class BasicResponse<TRequest>
    {
        public BasicResponse(DateTime startTime, TRequest request)
        {
            Start = startTime;
            Duration = TimeSpan.Zero;
            Messages = new List<string>();
            Request = request;
        }
        public TRequest Request { get; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> Messages { get; set; }
        public StatusEnum Status { get; set; }

        public BasicResponse<TRequest> Finalize(DateTime finish)
        {
            this.Duration = finish - Start;

            return this;
        }

        public BasicResponse<TRequest> Finalize()
        {
            return Finalize(DateTime.Now);
        }
    }
}
