namespace GalacticSenate.Library.Services.Gender.Requests {
   public class ReadGenderMultiRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadGenderRequest
    {
        public int Id { get; set; }
    }

    public class ReadGenderValueRequest
    {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
}
