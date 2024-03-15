Game Service Manager dùng để quản lý các lớp manager (Các lớp mà chúng ta thường sử dụng singleton) trong game

Ý tưởng xây dựng là sử dụng duy nhất 1 lớp master singleton để cung gấp khả năng truy cập tới các lớp manager khác nhau.
(Thay vì 1 class bất kì có thể truy cập tới bất kì singleton nào, thì giờ đây nó phải truy cập trung gian qua 1 lớp trừu tượng)


Ưu điểm:
+ Giảm tràn bộ nhớ do lạm dụng static
+ Cung cấp một nơi tập trung để quản lý các phần phụ thuộc và dịch vụ, giúp quản lý và sắp xếp code dễ dàng hơn.
+ Sự kết hợp tốt giữa tính đơn giản của mẫu Singleton và tính linh hoạt của Dependency Insert.
+ Dễ kiểm thử hơn Singleton

Yêu cầu:
+ Tạo 1 game object trên scene thêm script GameServiceManager vào game object đó
Khi cần thêm 1 service nào chỉ cần: 
+ Tạo 1 game object con của GameServiceManager
+ Thêm script manager kế thừa từ GameService vào object con vừa tạo
+ Dành cho fanboy OOP: tạo các interface cho service để giảm sự phụ thuộc, phù hợp khi muốn sửa đổi code manager

Câu lệnh:
+ GetService<T>: để get service từ GameServiceManager
+ ClearServices: để clear tất cả các service, tránh rò rỉ bộ nhớ