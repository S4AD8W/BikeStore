using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BikeStore.core.Domain.Product_NS;

namespace BikeStore.core.Domain {
  public class Cart {

    private List<CartLine> lineCollection = new List<CartLine>();

    public virtual void AddItem(BikeStore.core.Domain.Product_NS.Product product, int quantity) {
      CartLine line = lineCollection
          .Where(p => p.Product.IdxProduct == product.IdxProduct)
          .FirstOrDefault();

      if (line == null) {
        lineCollection.Add(new CartLine {
          Product = product,
          Quantity = quantity
        });
      } else {
        line.Quantity += quantity;
      }
    }

    public bool ChangeQuantiti (int xIdxProduct, int xQuantiti) {

      CartLine pLine = lineCollection.FirstOrDefault(x => x.Product.IdxProduct == xIdxProduct);

      if (pLine == null) return false;

      pLine.Quantity = xQuantiti;
      return true;

    }

    public bool RemoveItem (int xIdxProduct) {

      CartLine pLine = lineCollection.FirstOrDefault(x => x.Product.IdxProduct == xIdxProduct);

      if (pLine == null) return false;

      lineCollection.Remove(pLine);
      return true;

    }
    public virtual void RemoveLine(BikeStore.core.Domain.Product_NS.Product product) =>
        lineCollection.RemoveAll(l => l.Product.IdxProduct ==
            product.IdxProduct);

    public virtual double ComputeTotalValue() =>
        lineCollection.Sum(e => e.Product.Price * e.Quantity);

    public virtual void Clear() => lineCollection.Clear();

    public virtual IEnumerable<CartLine> Lines => lineCollection;
  }
}
