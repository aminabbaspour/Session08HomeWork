using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheAdapter.Settings
{
    public class CacheSettings
    {
        public bool UseDistributedCache { get; set; }
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }
}
