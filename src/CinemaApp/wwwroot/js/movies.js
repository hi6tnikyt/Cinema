document.addEventListener("DOMContentLoaded", function () {
    console.log("✅ DOM fully loaded and parsed.");

    // 1. Дефинираме елементите
    const modalElement = document.getElementById("movieDetailsModal");
    const detailsContainer = document.getElementById("movieDetailsContent");
    const modalWatchlistBtn = document.getElementById("add-to-watchlist-btn"); // Добавено

    if (!modalElement) {
        console.error("❌ Error: Modal element #movieDetailsModal not found!");
        return;
    }

    const movieDetailsModal = new bootstrap.Modal(modalElement);
    const viewDetailsButtons = document.querySelectorAll(".view-details-btn");

    viewDetailsButtons.forEach(button => {
        button.addEventListener("click", function (event) {
            event.preventDefault();
            event.stopPropagation();

            let movieId = this.getAttribute("data-movie-id");
            console.log(`🎬 Fetching details for movie ID: ${movieId}`);

            // Почистваме стария контент преди зареждане
            detailsContainer.innerHTML = '<div class="text-center"><div class="spinner-border" role="status"></div></div>';
            movieDetailsModal.show();

            fetch(`/movies/get-details/${movieId}`)
                .then(response => {
                    if (!response.ok) throw new Error(`Status: ${response.status}`);
                    return response.text();
                })
                .then(data => {
                    detailsContainer.innerHTML = data;

                    // Динамично заглавие
                    let movieTitleElement = detailsContainer.querySelector("h3");
                    document.getElementById("movieDetailsLabel").textContent =
                        movieTitleElement ? movieTitleElement.textContent : "Movie Details";

                    // Логика за Watchlist бутона вътре в модала
                    if (modalWatchlistBtn) {
                        modalWatchlistBtn.setAttribute("data-movie-id", movieId);

                        if (window.location.pathname.includes("/Watchlist")) {
                            modalWatchlistBtn.style.display = "none";
                        } else {
                            fetch(`/Watchlist/IsMovieInWatchlist/${movieId}`)
                                .then(res => res.json())
                                .then(isInWatchlist => {
                                    modalWatchlistBtn.style.display = isInWatchlist ? "none" : "inline-block";
                                })
                                .catch(err => console.error("⚠️ Watchlist status error:", err));
                        }
                    }

                    // Прикачваме слушателите за новите елементи
                    attachDynamicEventListeners();
                })
                .catch(error => {
                    console.error("❌ Fetch error:", error);
                    detailsContainer.innerHTML = `<p class="text-danger">Error loading details.</p>`;
                });
        });
    });
});

// Тази функция е ок, но я държим извън DOMContentLoaded за достъпност
function attachDynamicEventListeners() {
    setTimeout(() => {
        $("#add-to-watchlist-btn").off("click").on("click", function () {
            let movieId = $(this).attr("data-movie-id"); // Използваме .attr за по-сигурно при динамични данни

            if (!movieId) {
                Swal.fire("Error!", "Movie ID is missing.", "error");
                return;
            }

            $.post("/Watchlist/AddToWatchlist", { movieId: movieId })
                .done(function () {
                    Swal.fire({
                        title: "Added!",
                        text: "The movie has been added to your watchlist.",
                        icon: "success",
                        confirmButtonColor: "#28a745"
                    });
                    // Опционално: скриваме бутона след добавяне
                    $("#add-to-watchlist-btn").hide();
                })
                .fail(function () {
                    Swal.fire("Error!", "Could not add the movie.", "error");
                });
        });
    }, 100);
}

// jQuery за бутоните в главния списък (извън модала)
$(document).ready(function () {
    $(".add-to-watchlist-btn").on("click", function (e) {
        e.preventDefault();
        const movieId = $(this).data("movie-id");

        $.post("/Watchlist/AddToWatchlist", { movieId: movieId })
            .done(function () {
                Swal.fire("Added!", "Movie added to watchlist.", "success");
            })
            .fail(function () {
                Swal.fire("Error!", "Action failed.", "error");
            });
    });
});