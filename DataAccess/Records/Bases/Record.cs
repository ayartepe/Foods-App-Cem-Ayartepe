namespace DataAccess.Records.Bases
{
    public abstract class Record
	{

		public int Id { get; set; } // property, is required

		public string? Guid { get; set; } = System.Guid.NewGuid().ToString(); // property, is not required
    }
}
