using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain.Product_NS {
  public class Product {


    [Key]
    public int IdxProduct { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int IdxCategory { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime EditAt { get; private set; }
    public int Quantity { get; private set; }
    

    public Product() {

    }

    public Product(string xNe, string xDescription, decimal xPrice, int xIdxCategory, int xQuantity) {
      Name = xNe;
      Description = xDescription;
      Price = xPrice;
      IdxCategory = xIdxCategory;
      this.CreateAt = DateTime.UtcNow;
      this.EditAt = DateTime.UtcNow;
      this.Quantity = xQuantity;
    }

    public Product(int xIdxProduct, string xName, string xDescription, decimal xPrice, int xIdxCategory, int xQuantity, DateTime xCreateAT) {
      this.IdxProduct = xIdxProduct;
      this.Name = xName;
      this.Description = xDescription;
      this.Price = xPrice;
      this.IdxCategory = xIdxCategory;
      this.EditAt = DateTime.UtcNow;
      this.Quantity = xQuantity;
      this.CreateAt = xCreateAT;
    }

    [JsonConstructor]
    public Product(int idxProduct, string name, string description, decimal price, int idxCategory, DateTime createAt, DateTime editAt, int quantity) {
      IdxProduct = idxProduct;
      Name = name;
      Description = description;
      Price = price;
      IdxCategory = idxCategory;
      CreateAt = createAt;
      EditAt = editAt;
      Quantity = quantity;
    }
  }


}
