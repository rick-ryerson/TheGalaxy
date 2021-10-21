using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library
{
    public enum StatusEnum
    {
        NotSet,
        Successful,
        Failed
    }
    public class ModelResponse<TModel> : BasicResponse
    {
        public ModelResponse(DateTime startTime) : base(startTime)
        {
        }

        public List<TModel> Results { get; set; } = new List<TModel>();

        public new ModelResponse<TModel> Finalize()
        {
            return (ModelResponse<TModel>)base.Finalize();
        }
        new ModelResponse<TModel> Finalize(DateTime finish)
        {
            return (ModelResponse<TModel>)base.Finalize(finish);
        }
    }

    public class BasicResponse
    {
        public BasicResponse(DateTime startTime)
        {
            Start = startTime;
            Duration = TimeSpan.Zero;
            Messages = new List<string>();
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> Messages { get; set; }

        public StatusEnum Status { get; set; }

        public BasicResponse Finalize(DateTime finish)
        {
            this.Duration = finish - Start;

            return this;
        }

        public BasicResponse Finalize()
        {
            return Finalize(DateTime.Now);
        }
    }
}
