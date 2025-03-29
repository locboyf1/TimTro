$(document).ready(function () {
    // Kích hoạt Nice Select
    $("select").niceSelect();

    // Hàm tải danh sách tỉnh/thành phố
    function loadProvinces() {
        $.ajax({
            url: 'https://provinces.open-api.vn/api/?depth=1',
            method: 'GET',
            success: function (data) {
                var $provinceSelect = $('#province');
                $provinceSelect.html('<option value="">Chọn tỉnh/thành phố</option>'); // Reset options

                $.each(data, function (index, province) {
                    $provinceSelect.append(`<option value="${province.code}">${province.name}</option>`);
                });

                $provinceSelect.niceSelect('update');
            },
            error: function (xhr, status, error) {
                console.error("Lỗi khi tải danh sách tỉnh:", error);
            }
        });
    }

    // Hàm tải danh sách huyện/quận
    function loadDistricts(provinceCode) {
        $.ajax({
            url: `https://provinces.open-api.vn/api/p/${provinceCode}?depth=2`,
            method: 'GET',
            success: function (data) {
                var $districtSelect = $('#district');
                $districtSelect.html('<option value="">Chọn quận/huyện</option>');

                $.each(data.districts, function (index, district) {
                    $districtSelect.append(`<option value="${district.code}">${district.name}</option>`);
                });

                $districtSelect.niceSelect('update');
                $('#ward').html('<option value="">Chọn phường/xã</option>').niceSelect('update'); // Reset xã
            },
            error: function (xhr, status, error) {
                console.error("Lỗi khi tải danh sách huyện:", error);
            }
        });
    }

    // Hàm tải danh sách xã/phường
    function loadWards(districtCode) {
        $.ajax({
            url: `https://provinces.open-api.vn/api/d/${districtCode}?depth=2`,
            method: 'GET',
            success: function (data) {
                var $wardSelect = $('#ward');
                $wardSelect.html('<option value="">Chọn phường/xã</option>');

                $.each(data.wards, function (index, ward) {
                    $wardSelect.append(`<option value="${ward.code}">${ward.name}</option>`);
                });

                $wardSelect.niceSelect('update');
            },
            error: function (xhr, status, error) {
                console.error("Lỗi khi tải danh sách xã:", error);
            }
        });
    }

    // Gọi hàm load danh sách tỉnh/thành phố khi trang tải
    loadProvinces();

    // Sự kiện khi chọn tỉnh/thành phố
    $("#province").on("change", function () {
        var provinceCode = $(this).val();
        if (provinceCode) {
            loadDistricts(provinceCode);
        } else {
            $("#district").html('<option value="">Chọn quận/huyện</option>').niceSelect('update');
            $("#ward").html('<option value="">Chọn phường/xã</option>').niceSelect('update');
        }
    });

    // Sự kiện khi chọn huyện/quận
    $("#district").on("change", function () {
        var districtCode = $(this).val();
        if (districtCode) {
            loadWards(districtCode);
        } else {
            $("#ward").html('<option value="">Chọn phường/xã</option>').niceSelect('update');
        }
    });
});
