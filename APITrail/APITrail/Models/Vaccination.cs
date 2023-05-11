namespace TodoApi.Models;
public class Vaccination
{
    public Vaccination()
    { }
    public int Id { get; set; }
    public int W_Id { get; set; }
    public DateTime Moed { get; set; }
    public string Creator { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}