package com.example.ecommerce.repository;

import com.example.ecommerce.entity.Product;
import io.quarkus.hibernate.orm.panache.PanacheRepository;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.transaction.Transactional;

import java.util.List;

@ApplicationScoped
public class ProductRepository implements PanacheRepository<Product> {

    // Find all products by name
    public List<Product> findByName(String name) {
        return list("name", name); // Equivalent to: SELECT * FROM Product WHERE name = :name
    }

    // Find products within a price range
    public List<Product> findByPriceRange(double minPrice, double maxPrice) {
        return list("price >= ?1 and price <= ?2", minPrice, maxPrice); // SQL-like parameterized query
    }

    // Count all products
    public long countAllProducts() {
        return count(); // Counts the number of products in the database
    }

    // Check if a product exists by ID
    public boolean exists(Long id) {
        return findByIdOptional(id).isPresent();
    }

     @PersistenceContext
    EntityManager entityManager;

    @Transactional
    public void addProduct(Product product) {
        entityManager.persist(product);
    }
}
