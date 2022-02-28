namespace Xtramile.WeatherApp.Common.Dtos
{
    public class AppResultDto
    {
        public bool Succeeded { get; set; }
        public int Status { get; set; }
        public string[] Errors { get; set; }
    }

    public class AppResultDto<T> : AppResultDto
    {
        public T Data { get; set; }
    }
}
