$(document).ready(function () {
    $(".buy-ticket-btn").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();

        const cinemaId = $(this).attr("data-cinema-id");
        const cinemaName = $(this).attr("data-cinema-name");
        const movieId = $(this).attr("data-movie-id");
        const movieName = $(this).attr("data-movie-name");

        console.log("Cinema ID:", cinemaId);
        console.log("Cinema Name:", cinemaName);
        console.log("Movie ID:", movieId);
        console.log("Movie Name:", movieName);

        if (!cinemaId || !movieId) {
            Swal.fire("Error!", "Missing Cinema ID or Movie ID.", "error");
            return;
        }

        $("#cinemaId").val(cinemaId);
        $("#movieId").val(movieId);
        $("#cinemaNamePlaceholder").text(cinemaName);

        const showTimesSelect = $("#showtime");
        showTimesSelect.find("option").remove() 
        .append('<option value="">Select Showtime...</option>');
        $.ajax({
            url: `/api/MovieApi/GetShowTimes?movieId=${movieId}&cinemaId=${cinemaId}`,
            method: "GET",
            success: function (response) {
                for (const showtime of response) {
                   
                    showTimesSelect.append(new Option(showtime, showtime));
                }
            },
            error: function (xhr) {
                let errorMessage = "An error occurred while purchasing tickets.";
                console.error("Raw Response:", xhr.responseText);

                try {
                    if (xhr.responseJSON) {
                        errorMessage = xhr.responseJSON.title || xhr.responseJSON.message
                            || errorMessage;
                    } else if (xhr.responseText) {
                        errorMessage = xhr.responseText;
                    }
                } catch (e) {
                    console.error("Error processing response:", e);
                }

                Swal.fire("Error!", errorMessage, "error");
            }
        }); 

        $("#buyTicketModalLabel").html(`Buy Ticket - ${cinemaName} <br><small class="text-muted">${movieName}</small>`);

        $("#buyTicketModal").modal("show");
    });
});

$("#buyTicketButton").on("click", function (e) {
    e.preventDefault();
    e.stopPropagation();

    const requestData = {
        cinemaId: $("#cinemaId").val().trim(),
        movieId: $("#movieId").val().trim(),
        quantity: parseInt($("#quantity").val(), 10),
        showtime: $("#showtime").find(":selected").val(),
    };


    console.log("Submitting Request:", requestData); 

    if (!requestData.quantity || requestData.quantity < 1) {
        $("#errorMessage").text("Please enter a valid ticket quantity.").removeClass("d-none");
        return;
    }

    $.ajax({
        url: "/api/TicketApi/BuyTicket",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(requestData),
        headers: {
            "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            console.log("Success Response:", response); 

            Swal.fire("Success!", "Your ticket has been purchased successfully!", "success");
            $("#buyTicketModal").modal("hide");
        },
        error: function (xhr) {
            let errorMessage = "An error occurred while purchasing tickets.";
            console.error("Raw Response:", xhr.responseText); 

            try {
                if (xhr.responseJSON) {
                    errorMessage = xhr.responseJSON.title || xhr.responseJSON.message
                        || errorMessage;
                } else if (xhr.responseText) {
                    errorMessage = xhr.responseText; 
                }
            } catch (e) {
                console.error("Error processing response:", e);
            }

            Swal.fire("Error!", errorMessage, "error");
        }
    }); 
}); 