namespace APBD10.DTOs;

public record CreateProductDTO(string ProductName, double ProductWeight, double ProductWidth,
    double ProductHeight, double ProductDepth, List<int> ProductCategories);