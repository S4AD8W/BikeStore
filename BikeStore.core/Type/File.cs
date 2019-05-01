using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.core.Type {
public class File {

    public long Size { get; private set; }
    public string Name { get; private set; } 
    public byte[] Contet { get; private set; }

    public File(long xSize, string xName, byte[] xContent) {
      this.Size = xSize;
      this.Name = xName;
      this.Contet = xContent;
    }
  }
}
