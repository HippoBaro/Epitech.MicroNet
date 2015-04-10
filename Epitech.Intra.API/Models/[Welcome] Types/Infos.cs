using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data.WelcomeJsonTypes
{

    public class Infos
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("email")]
        public object Email { get; set; }

        [JsonProperty("internal_email")]
        public string InternalEmail { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("userinfo")]
        public string Userinfo { get; set; }

        [JsonProperty("referent_used")]
        public bool ReferentUsed { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("picture_fun")]
        public object PictureFun { get; set; }

        [JsonProperty("email_referent")]
        public string EmailReferent { get; set; }

        [JsonProperty("pass_referent")]
        public string PassReferent { get; set; }

        [JsonProperty("promo")]
        public int Promo { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }

        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("gid")]
        public int Gid { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("documents")]
        public object Documents { get; set; }

        [JsonProperty("userdocs")]
        public string Userdocs { get; set; }

        [JsonProperty("shell")]
        public object Shell { get; set; }

        [JsonProperty("netsoul")]
        public object Netsoul { get; set; }

        [JsonProperty("close")]
        public bool Close { get; set; }

        [JsonProperty("close_reason")]
        public object CloseReason { get; set; }

        [JsonProperty("ctime")]
        public string Ctime { get; set; }

        [JsonProperty("mtime")]
        public object Mtime { get; set; }

        [JsonProperty("comment")]
        public object Comment { get; set; }

        [JsonProperty("id_promo")]
        public string IdPromo { get; set; }

        [JsonProperty("id_history")]
        public string IdHistory { get; set; }

        [JsonProperty("course_code")]
        public string CourseCode { get; set; }

        [JsonProperty("school_code")]
        public string SchoolCode { get; set; }

        [JsonProperty("school_title")]
        public string SchoolTitle { get; set; }

        [JsonProperty("old_id_promo")]
        public string OldIdPromo { get; set; }

        [JsonProperty("old_id_location")]
        public string OldIdLocation { get; set; }

        [JsonProperty("rights")]
        public Rights Rights { get; set; }

        [JsonProperty("invited")]
        public bool Invited { get; set; }

        [JsonProperty("studentyear")]
        public int Studentyear { get; set; }

        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("decoded")]
        public int Decoded { get; set; }
    }

}
