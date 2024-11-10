const canvas = document.getElementById("gameCanvas");
const ctx = canvas.getContext("2d");

// Rocket properties
let rocketX = 200;
let rocketY = 50;
let rocketVelocityY = 0;
let rocketVelocityX = 0;
const gravity = 0.02;
const thrustPower = -0.08;
const horizontalThrust = 0.05;
let thrusting = false;
let leftThrust = false;
let rightThrust = false;
const rocketSize = 10;

// Terrain properties
const groundY = 450;
const landingPadX = 180;
const landingPadWidth = 40;

// Game loop
function update() {
    // Apply gravity
    rocketVelocityY += gravity;

    // Apply thrust
    if (thrusting) rocketVelocityY += thrustPower;
    if (leftThrust) rocketVelocityX -= horizontalThrust;
    if (rightThrust) rocketVelocityX += horizontalThrust;

    // Update rocket position
    rocketX += rocketVelocityX;
    rocketY += rocketVelocityY;

    // Boundary checks
    if (rocketX < 0) rocketX = 0;
    if (rocketX > canvas.width - rocketSize) rocketX = canvas.width - rocketSize;

    // Check for landing
    if (rocketY + rocketSize >= groundY) {
        if (rocketX > landingPadX && rocketX < landingPadX + landingPadWidth && rocketVelocityY < 1) {
            alert("Successful Landing!");
        } else {
            alert("Crash! Game Over.");
        }
        resetGame();
    }

    draw();
}

// Draw everything
function draw() {
    // Clear canvas
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Draw terrain
    ctx.strokeStyle = "white";
    ctx.beginPath();
    ctx.moveTo(0, groundY);
    ctx.lineTo(landingPadX, groundY);
    ctx.lineTo(landingPadX + landingPadWidth, groundY);
    ctx.lineTo(canvas.width, groundY);
    ctx.stroke();

    // Draw mountains
    ctx.beginPath();
    ctx.moveTo(0, groundY);
    ctx.lineTo(100, 300);
    ctx.lineTo(200, groundY);
    ctx.lineTo(300, 350);
    ctx.lineTo(canvas.width, groundY);
    ctx.stroke();

    // Draw rocket
    ctx.beginPath();
    ctx.moveTo(rocketX, rocketY);
    ctx.lineTo(rocketX - rocketSize / 2, rocketY + rocketSize);
    ctx.lineTo(rocketX + rocketSize / 2, rocketY + rocketSize);
    ctx.closePath();
    ctx.stroke();
}

// Reset the game
function resetGame() {
    rocketX = 200;
    rocketY = 50;
    rocketVelocityY = 0;
    rocketVelocityX = 0;
    thrusting = false;
    leftThrust = false;
    rightThrust = false;
}

// Handle key events
window.addEventListener("keydown", (e) => {
    if (e.key === "ArrowUp") thrusting = true;
    if (e.key === "ArrowLeft") leftThrust = true;
    if (e.key === "ArrowRight") rightThrust = true;
});

window.addEventListener("keyup", (e) => {
    if (e.key === "ArrowUp") thrusting = false;
    if (e.key === "ArrowLeft") leftThrust = false;
    if (e.key === "ArrowRight") rightThrust = false;
});

// Start game loop
setInterval(update, 16);
