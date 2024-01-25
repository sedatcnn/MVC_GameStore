$(document).ready(function () {
    $('.buyume').hover(
        function () {
            $(this).css({
                'transform': 'scale(1.1)',
                'transition': 'transform 0.3s ease-in-out' 
            });
        },
        function () {
            $(this).css({
                'transform': 'scale(1)',
                'transition': 'transform 0.3s ease-in-out' 
            });
        }
    );
});
