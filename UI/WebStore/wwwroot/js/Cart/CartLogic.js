Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink:"",
        decrementLink:"",
        removeFromCartLink:"",
    },

    init: function(properties) {
        $.extend(Cart._properties, properties);

        Cart.initEvents();

    },

    initEvents: function () {

        $(".add-to-cart").click(Cart.addToCart);

        $(".cart-quantity-up").click(Cart.incrementItem);
        $(".cart-quantity-down").click(Cart.decrementItem);
        $(".cart-quantity-delete").click(Cart.removeItem);
    },

    addToCart: function(event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function() {
                Cart.showToolTip(button);
                Cart.refreshCartView();
            })
            .fail(function() { console.log("addToCart fail") });
    },

    showToolTip: function (button) {
        button.tooltip({ title: "Added to cart" }).tooltip("show");
        setTimeout(function() {
                button.tooltip("destroy");
            },
            500);
    },

    refreshCartView() {
        var container = $("#cart-container");
        $.get(Cart._properties.getCartViewLink)
            .done(function(cartHtml) {
                container.html(cartHtml);
            })
            .fail(function () { console.log("refreshCartView fail") });
    },

    incrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        var container = button.closest("tr");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                const count = parseInt($(".cart-quantity-input", container).val());
                $(".cart-quantity-input", container).val(count + 1);


                Cart.refreshPrice(container);
                Cart.refreshCartView();
            })
            .fail(function () { console.log("incrementItem fail") });
    },

    refreshPrice: function(container) {
        const count = parseInt($(".cart-quantity-input", container).val());
        const price = parseFloat($(".cart-price", container).data("price"));

        const totalPrice = count * price;

        const value = totalPrice.toLocaleString("ru=RU", { style: "currency", currency: "RUB" });
        const cartTotalPrice = $(".cart-total-price", container);
        cartTotalPrice.data("price", totalPrice);
        cartTotalPrice.html(value);

        Cart.refreshPrice();
    },

    refreshTotalPrice: function() {
        var totalPrice = 0;

        $(".cart-total-price").each(function() {
            const price = parseFloat($(this).data("price"));
            totalPrice += price;
        });

        const value = totalPrice.toLocaleString("ru=RU", { style: "currency", currency: "RUB" });
        $("#total-order-price").html(value);
    },

    decrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        var container = button.closest("tr");

        $.get(Cart._properties.decrementLink + "/" + id)
            .done(function () {
                const count = parseInt($(".cart-quantity-input", container).val());

                if (count > 1) {
                    $(".cart-quantity-input", container).val(count - 1);
                    Cart.refreshPrice(container);
                } else {
                    container.remove();
                    Cart.refreshTotalPrice();
                }



                Cart.refreshCartView();
            })
            .fail(function () { console.log("decrementItem fail") });
    },

    removeItem: function (event) {
        event.preventDefault();

        var button = $(this);

        const id = button.data("id");

        var container = button.closest("tr");

        $.get(Cart._properties.removeFromCartLink + "/" + id)
            .done(function () {
                button.closest("tr").remove();

                Cart.refreshTotalPrice();
                Cart.refreshCartView();
            })
            .fail(function () { console.log("removeItem fail") });
    },
}