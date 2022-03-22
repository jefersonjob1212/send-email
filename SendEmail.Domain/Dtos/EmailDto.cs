namespace SendEmail.Domain.Dtos;

public class EmailDto
{
    public string ToName { get; set; }
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}
