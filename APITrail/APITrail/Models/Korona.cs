namespace TodoApi.Models;
public class Korona
{
    public Korona()
    {

    }
    public int Id { get; set; }
    public int W_Id { get; set; }
    public DateTime Positive { get; set; }
    public DateTime Negative { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}