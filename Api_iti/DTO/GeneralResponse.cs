namespace Api_iti.DTO
{
    //General Response / Standard Response Pattern
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
    }
}
