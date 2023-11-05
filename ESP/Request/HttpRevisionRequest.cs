namespace ESP.Request
{
	public class HttpRevisionRequest
	{
		public int Id { get; set; }

		public int ProcessId { get; set; }
		public string ModificationName { get; set; } = string.Empty;

		public string RevisionType { get; set; } = String.Empty;
		public string PrimaryModification { get; set; } = string.Empty;

		public bool? Current { get; set; }

		public string StartDate { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;

		public string ProcessInfo { get; set; } = string.Empty;
		public string ProcessCode { get; set; } = string.Empty;
		public string Profile { get; set; } = string.Empty;
		public string RsOneCode { get; set; } = string.Empty;
		public string RsOneName { get; set; } = string.Empty;
		public string GroupCode { get; set; } = string.Empty;
		public string CheckCall { get; set; } = string.Empty;
		public string RequestDate { get; set; } = string.Empty;
		public string DirectionDate { get; set; } = string.Empty;
		public string ResultDate { get; set; } = string.Empty;
		public string EndDate { get; set; } = string.Empty;
		public bool? Note { get; set; }
		public string ApprovedProfile { get; set; } = string.Empty;
		public string ApprovedProm { get; set; } = string.Empty;
		public string ApprovedWithNote { get; set; } = string.Empty;
		public string integrated { get; set; } = string.Empty;
		public string ContactName { get; set; } = string.Empty;
		
		public string ResponsibleOKBP { get; set; } = string.Empty;
		public string Subjects { get; set; } = string.Empty;

		public string ResponsibleTechnologist { get; set; } = string.Empty;

	}
}
