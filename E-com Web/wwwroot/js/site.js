// Update cart count on page load
document.addEventListener('DOMContentLoaded', function() {
    updateCartCount();
});

// Function to update cart count
async function updateCartCount() {
    try {
        const response = await fetch('/Cart/GetCartCount');
        const data = await response.json();
        const cartCountElement = document.getElementById('cart-count');
        if (cartCountElement) {
            cartCountElement.textContent = data.count;
        }
    } catch (error) {
        console.error('Error updating cart count:', error);
    }
}

// Update cart count after adding to cart
document.addEventListener('submit', function(event) {
    const form = event.target;
    if (form.action && form.action.includes('/Cart/AddToCart')) {
        setTimeout(updateCartCount, 500);
    }
});

