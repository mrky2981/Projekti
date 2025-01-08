package com.example.ecommerce.resource;

import java.util.List;

import com.example.ecommerce.entity.Product;
import com.example.ecommerce.repository.ProductRepository;

import jakarta.inject.Inject;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.DELETE;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.PathParam;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

@Path("/products")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class ProductResource {

    @Inject
    ProductRepository productRepository;

    @GET
    public List<Product> getAllProducts() {
        return productRepository.listAll();
    }

   @Path("addProduct")
    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    public Response addProduct(Product product) {
        try {
            productRepository.addProduct(product);
            return Response.status(Response.Status.CREATED).entity("Product created successfully!").build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR).entity("Failed to create product").build();
        }
    }
    @DELETE
    @Path("/delete{id}")
    public void deleteProduct(@PathParam("id") Long id) {
        productRepository.deleteById(id);
    }
    
}
