using Newtonsoft.Json;
using Epitech.Intra.API.Data.EventJsonTypes;

namespace Epitech.Intra.API.Data
{
	public class Event
	{
		[JsonProperty ("scolaryear")]
		public string Scolaryear { get; set; }

		[JsonProperty ("codemodule")]
		public string Codemodule { get; set; }

		[JsonProperty ("codeinstance")]
		public string Codeinstance { get; set; }

		[JsonProperty ("codeacti")]
		public string Codeacti { get; set; }

		[JsonProperty ("codeevent")]
		public string Codeevent { get; set; }

		[JsonProperty ("semester")]
		public int Semester { get; set; }

		[JsonProperty ("instance_location")]
		public string InstanceLocation { get; set; }

		[JsonProperty ("module_title")]
		public string ModuleTitle { get; set; }

		[JsonProperty ("acti_title")]
		public string ActiTitle { get; set; }

		[JsonProperty ("acti_description")]
		public string ActiDescription { get; set; }

		[JsonProperty ("type_title")]
		public string TypeTitle { get; set; }

		[JsonProperty ("type_code")]
		public string TypeCode { get; set; }

		[JsonProperty ("allowed_planning_start")]
		public string AllowedPlanningStart { get; set; }

		[JsonProperty ("allowed_planning_end")]
		public string AllowedPlanningEnd { get; set; }

		[JsonProperty ("nb_hours")]
		public string NbHours { get; set; }

		[JsonProperty ("nb_group")]
		public int NbGroup { get; set; }

		[JsonProperty ("has_exam_subject")]
		public bool HasExamSubject { get; set; }

		[JsonProperty ("begin")]
		public string Begin { get; set; }

		[JsonProperty ("start")]
		public string Start { get; set; }

		[JsonProperty ("end")]
		public string End { get; set; }

		[JsonProperty ("num_event")]
		public int NumEvent { get; set; }

		[JsonProperty ("title")]
		public object Title { get; set; }

		[JsonProperty ("description")]
		public object Description { get; set; }

		[JsonProperty ("nb_registered")]
		public int NbRegistered { get; set; }

		[JsonProperty ("id_dir")]
		public object IdDir { get; set; }

		[JsonProperty ("room")]
		public Room Room { get; set; }

		[JsonProperty ("seats")]
		public object Seats { get; set; }

		[JsonProperty ("desc_webservice")]
		public string DescWebservice { get; set; }

		[JsonProperty ("name_bocal")]
		public string NameBocal { get; set; }
	}

}
