using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.CalendarJsonTypes;
using Newtonsoft.Json.Converters;

namespace Epitech.Intra.API.Data
{

	class CustomDateTimeConverter : IsoDateTimeConverter
	{
		public CustomDateTimeConverter ()
		{
			base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
		}
	}

	public class RegisterStudent
	{
		[JsonProperty ("id")]
		public string id { get; set; }

		[JsonProperty ("login")]
		public string login { get; set; }

		[JsonProperty ("title")]
		public string title { get; set; }

		[JsonProperty ("picture")]
		public string picture { get; set; }

		[JsonProperty ("present")]
		public object present { get; set; }

		[JsonProperty ("token_trace")]
		public object token_trace { get; set; }

		[JsonProperty ("can_token")]
		public string can_token { get; set; }

		[JsonProperty ("registered")]
		public string registered { get; set; }
	}

	public class Calendar
	{
		public int ID { get; set; }

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

		[JsonProperty ("titlemodule")]
		public string Titlemodule { get; set; }

		[JsonProperty ("prof_inst")]
		public ProfInst[] ProfInst { get; set; }

		[JsonProperty ("acti_title")]
		public string ActiTitle { get; set; }

		[JsonProperty ("num_event")]
		public int NumEvent { get; set; }

		[JsonProperty ("start"), JsonConverter (typeof(CustomDateTimeConverter))]
		public DateTime Start { get; set; }

		[JsonProperty ("end"), JsonConverter (typeof(CustomDateTimeConverter))]
		public DateTime End { get; set; }

		[JsonProperty ("total_students_registered")]
		public int TotalStudentsRegistered { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("type_title")]
		public string TypeTitle { get; set; }

		[JsonProperty ("type_code")]
		public string TypeCode { get; set; }

		[JsonProperty ("is_rdv")]
		public string IsRdv { get; set; }

		[JsonProperty ("nb_hours")]
		public string NbHours { get; set; }

		[JsonProperty ("allowed_planning_start")]
		public string AllowedPlanningStart { get; set; }

		[JsonProperty ("allowed_planning_end")]
		public string AllowedPlanningEnd { get; set; }

		[JsonProperty ("nb_group")]
		public int NbGroup { get; set; }

		[JsonProperty ("nb_max_students_projet")]
		public string NbMaxStudentsProjet { get; set; }

		[JsonProperty ("room")]
		public Room Room { get; set; }

		[JsonProperty ("dates")]
		public object Dates { get; set; }

		[JsonProperty ("module_available")]
		public bool ModuleAvailable { get; set; }

		[JsonProperty ("module_registered")]
		public bool ModuleRegistered { get; set; }

		[JsonProperty ("past")]
		public bool Past { get; set; }

		[JsonProperty ("allow_register")]
		public bool AllowRegister { get; set; }

		[JsonProperty ("event_registered")]
		public string EventRegistered { get; set; }

		[JsonProperty ("project")]
		public bool Project { get; set; }

		[JsonProperty ("rdv_group_registered")]
		public object RdvGroupRegistered { get; set; }

		[JsonProperty ("rdv_indiv_registered")]
		public object RdvIndivRegistered { get; set; }

		[JsonProperty ("allow_token")]
		public bool AllowToken { get; set; }

		public bool TokenAsked { get; set; }

		[JsonProperty ("register_student")]
		public bool RegisterStudent { get; set; }

		[JsonProperty ("register_prof")]
		public object RegisterProf { get; set; }

		[JsonProperty ("register_month")]
		public object RegisterMonth { get; set; }

		[JsonProperty ("in_more_than_one_month")]
		public bool InMoreThanOneMonth { get; set; }

		public string EventKitID { get; set; }

		public bool RegisterEventForStoring { get; set; }
	}

}
