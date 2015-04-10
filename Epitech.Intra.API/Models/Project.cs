using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.ProjectJsonTypes;

namespace Epitech.Intra.API.Data
{

    public class Project
    {

        [JsonProperty("scolaryear")]
        public string Scolaryear { get; set; }

        [JsonProperty("codemodule")]
        public string Codemodule { get; set; }

        [JsonProperty("codeinstance")]
        public string Codeinstance { get; set; }

        [JsonProperty("codeacti")]
        public string Codeacti { get; set; }

        [JsonProperty("instance_location")]
        public string InstanceLocation { get; set; }

        [JsonProperty("module_title")]
        public string ModuleTitle { get; set; }

        [JsonProperty("id_activite")]
        public string IdActivite { get; set; }

        [JsonProperty("project_title")]
        public string ProjectTitle { get; set; }

        [JsonProperty("type_title")]
        public string TypeTitle { get; set; }

        [JsonProperty("type_code")]
        public string TypeCode { get; set; }

        [JsonProperty("register")]
        public bool Register { get; set; }

        [JsonProperty("register_by_bloc")]
        public string RegisterByBloc { get; set; }

        [JsonProperty("register_prof")]
        public string RegisterProf { get; set; }

        [JsonProperty("nb_min")]
        public int NbMin { get; set; }

        [JsonProperty("nb_max")]
        public int NbMax { get; set; }

        [JsonProperty("begin")]
        public DateTime Begin { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("end_register")]
        public string EndRegister { get; set; }

        [JsonProperty("deadline")]
        public object Deadline { get; set; }

        [JsonProperty("is_rdv")]
        public bool IsRdv { get; set; }

        [JsonProperty("instance_allowed")]
        public string InstanceAllowed { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("over")]
        public int Over { get; set; }

        [JsonProperty("over_deadline")]
        public object OverDeadline { get; set; }

        [JsonProperty("date_access")]
        public bool DateAccess { get; set; }

        [JsonProperty("instance_registered")]
        public string InstanceRegistered { get; set; }

        [JsonProperty("user_project_status")]
        public string UserProjectStatus { get; set; }

        [JsonProperty("note")]
        public object Note { get; set; }

        [JsonProperty("root_slug")]
        public string RootSlug { get; set; }

        [JsonProperty("forum_path")]
        public object ForumPath { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("call_ihk")]
        public string CallIhk { get; set; }

        [JsonProperty("nb_notes")]
        public string NbNotes { get; set; }

        [JsonProperty("user_project_master")]
        public string UserProjectMaster { get; set; }

        [JsonProperty("user_project_code")]
        public string UserProjectCode { get; set; }

        [JsonProperty("user_project_title")]
        public string UserProjectTitle { get; set; }

        [JsonProperty("registered_instance")]
        public int RegisteredInstance { get; set; }

        [JsonProperty("registered")]
        public Registered[] Registered { get; set; }

        [JsonProperty("notregistered")]
        public Notregistered[] Notregistered { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }

}
