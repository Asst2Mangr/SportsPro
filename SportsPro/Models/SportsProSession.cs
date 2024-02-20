using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SportsPro.Models
{
    public class SportsProSession : Controller
    {
        private const string TechKey ="techKey";

        private ISession session { get; set; }
        public SportsProSession(ISession session)
        {
            this.session = session;
        }

        public void setMyTech(int id)
        {
            session.SetInt32(TechKey,id);
        }
        public int getMyTech()
        {
            return (int)session.GetInt32(TechKey);
        }
    }
}
