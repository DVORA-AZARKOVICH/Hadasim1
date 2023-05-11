namespace TodoApi.Models;
public class Worker
{
    public Worker()
    {

    }
    public int Id { get; set; }
    public string? W_Name { get; set; }
    public string? Address { get; set; }
    public DateTime? Birth_Date { get; set; }
    public int? Telephone { set; get; }
    public int? Pelephone { set; get; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}