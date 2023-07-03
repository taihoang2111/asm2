using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public class MemberDAO : DbContext
    {
        public static MemberDAO instance = null;
        public static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                }
                return instance; }
        }

        internal void InsertMember(MemberObject member)
        {
            throw new NotImplementedException();
        }

        public DbSet<MemberObject> Members { get; set; }
        //--------------------------------------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MySaleDB"));
        }
        //--------------------------------------------------------
        public void AddNew(MemberObject member)
        {
            using (var context = MemberDAO.Instance)
            {
                context.Members.Add(member);
                context.SaveChanges();

            }
        }
        //--------------------------------------------------------
        public IEnumerable<MemberObject> GetMemberObjectsList()
        {
            using (var context = MemberDAO.Instance)
            {
                var mbl = context.Members.ToList();
                return mbl;
            }
        }
        //--------------------------------------------------------
        public MemberObject GetMemberObjectByID(int memberID)
        {
            using(var context = MemberDAO.Instance)
            {
                var mb = context.Members.FirstOrDefault(c => c.MemberID == memberID);
                if (mb != null)
                {
                    return mb;
                }
                else return null;
            }
        }
        //--------------------------------------------------------
        public void Update (MemberObject member)
        {
            using (var context = MemberDAO.Instance)
            {
                var mb = context.Members.FirstOrDefault(c => c.MemberID == member.MemberID);
                if(mb != null)
                {
                    mb = member;
                    context.SaveChanges();
                }
            }
        }
        //--------------------------------------------------------
        public void Delete (int memberID)
        {
            using (var context = MemberDAO.Instance)
            {
                var mb = context.Members.FirstOrDefault (c => c.MemberID == memberID);
                if( mb != null)
                {
                    context.Members.Remove(mb);
                    context.SaveChanges();
                }
            }
        }
        //--------------------------------------------------------
        //--------------------------------------------------------
        //--------------------------------------------------------
        //--------------------------------------------------------
        //--------------------------------------------------------
        //--------------------------------------------------------

    }
}
