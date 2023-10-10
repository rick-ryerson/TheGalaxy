namespace GalacticSenate.Library.Services.PersonNameType.Requests {
   public class ReadPersonNameTypeMultiRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadPersonNameTypeRequest
    {
        public int Id { get; set; }
    }

    public class ReadPersonNameTypeValueRequest
    {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
}
