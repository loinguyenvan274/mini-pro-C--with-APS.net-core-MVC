const sidebar = document.getElementById("f2-l");
const edgeArea = document.querySelector(".edge-hover");

// Khi di chuột gần mép trái
edgeArea.addEventListener("mouseenter", () => {
    sidebar.style.left = "0";
});

// Khi rời chuột khỏi sidebar
sidebar.addEventListener("mouseleave", () => {
    sidebar.style.left = "calc(-14vw + 10px)";
});


const button = document.getElementById("tao-khoa-hoc");
button.addEventListener("click", () => {
    fetch("/HocVien/LayThongTinHocVien", {
        method: "GET",
    }).then((response) => {
        if (response.ok) {
            alert("Đã tạo khóa học thành công!");
            location.reload(); // Tải lại trang
        } else {
            throw new Error("Network response was not ok.");
        }
    }).catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
    });
    if (response.ok) {
        return response.json();
    } 

    const taoKhoaHocElement = document.querySelector(".f2-tao-khoa-hoc");
    if (taoKhoaHocElement) {
        taoKhoaHocElement.style.zIndex = "9";
    }
});

document.querySelector(".f2-tao-khoa-hoc").addEventListener("click", (event) => {
    if (event.target === document.querySelector(".f2-tao-khoa-hoc")) {
        const taoKhoaHocElement = document.querySelector(".f2-tao-khoa-hoc");
        if (taoKhoaHocElement) {
            taoKhoaHocElement.style.zIndex = "0";
        }
    }
});
