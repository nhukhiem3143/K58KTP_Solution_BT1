# Nguyễn Như Khiêm - K225480106030
# K58ktp - Môn: Phát triển ứng dụng trên nền web
# BÀI TẬP VỀ NHÀ 01:
## TẠO SOLUTION GỒM CÁC PROJECT SAU:
1. DLL đa năng, keyword: c# window library -> Class Library (.NET Framework) bắt buộc sử dụng .NET Framework 2.0: giải bài toán bất kỳ, độc lạ càng tốt, phải có dấu ấn cá nhân trong kết quả, biên dịch ra DLL. DLL độc lập vì nó ko nhập, ko xuất, nó nhận input truyền vào thuộc tính của nó, và trả về dữ liệu thông qua thuộc tính khác, hoặc thông qua giá trị trả về của hàm. Nó độc lập thì sẽ sử dụng được trên app dạng console (giao diện dòng lệnh - đen sì), cũng sử dụng được trên app desktop (dạng cửa sổ), và cũng sử dụng được trên web form (web chạy qua iis).

2. Console app, bắt buộc sử dụng .NET Framework 2.0, sử dụng được DLL trên: nhập được input, gọi DLL, hiển thị kết quả, phải có dấu án cá nhân. keyword: c# window Console => Console App (.NET Framework), biên dịch ra EXE

3. Windows Form Application, bắt buộc sử dụng .NET Framework 2.0**, sử dụng được DLL đa năng trên, kéo các control vào để có thể lấy đc input, gọi DLL truyền input để lấy đc kq, hiển thị kq ra window form, phải có dấu án cá nhân; keyword: c# window Desktop => Windows Form Application (.NET Framework), biên dịch ra EXE
  
4. Web đơn giản, bắt buộc sử dụng .NET Framework 2.0, sử dụng web server là IIS, dùng file hosts để tự tạo domain, gắn domain này vào iis, file index.html có sử dụng html css js để xây dựng giao diện nhập được các input cho bài toán, dùng mã js để tiền xử lý dữ liệu, js để gửi lên backend. backend là api.aspx, trong code của api.aspx.cs thì lấy được các input mà js gửi lên, rồi sử dụng được DLL đa năng trên. kết quả gửi lại json cho client, js phía client sẽ nhận được json này hậu xử lý để thay đổi giao diện theo dữ liệu nhận dược, phải có dấu án cá nhân. keyword: c# window web => ASP.NET Web Application (.NET Framework) + tham khảo link chatgpt thầy gửi. project web này biên dịch ra DLL, phải kết hợp với IIS mới chạy được.

# Projects cần tạo 
## 1. Class Library (.NET Framework 2.0) — tạo DLL đa năng (ví dụ YourNameLib.dll):
+ Quan trọng: DLL độc lập — không đọc/ghi file, không sử dụng UI, không dùng console trực tiếp.
+ Nhận dữ liệu bằng thuộc tính public (ví dụ Input), trả kết quả bằng thuộc tính khác hoặc giá trị trả về của hàm (ví dụ Result, Execute() trả bool).
+ Biên dịch ra *.dll.

## 2. Console App (.NET Framework 2.0):
+ Gọi DLL: truyền input vào DLL qua thuộc tính/ hàm, lấy kết quả và in ra console.
+ Biên dịch ra EXE.

## 3. Windows Form Application (.NET Framework 2.0):
+ Giao diện lấy input (textbox, button), gọi DLL, show kết quả trong form (multiline textbox, label...).

## 4. Web App — ASP.NET Web Application (.NET Framework 2.0):
+ Client: index.html (HTML + CSS + JS) dùng để nhập input, tiền xử lý (nếu cần) bằng JS.
+ Client JS: gửi dữ liệu qua AJAX (ví dụ XMLHttpRequest hoặc fetch) tới api.aspx.
+ Server: api.aspx + api.aspx.cs (code-behind) — trong code-behind gọi DLL để xử lý dữ liệu và trả JSON cho client.
+ Web project build ra DLL (ASP.NET assemblies) và phải chạy qua IIS (cài site, bind host name bằng file hosts).

# Bài toán: Trò chơi Pikachu – tìm cặp hình giống nhau có thể nối bằng ≤ 3 đoạn thẳng  

<img width="1920" height="1080" alt="Screenshot 2025-09-26 210020" src="https://github.com/user-attachments/assets/7da2ddc9-d41d-461a-b85b-0795ddd4b4b5" />

# Các bước thực hiện 
## Bước 1 : Tạo Solution
1. Mở Visual Studio 2022.
2. Chọn File → New → Project.
3. Tìm template Blank Solution (hoặc gõ từ khóa "Blank Solution" trong ô tìm kiếm).
4. Đặt tên Solution : Pikachu → Create.

<img width="1140" height="988" alt="Screenshot 2025-09-26 211714" src="https://github.com/user-attachments/assets/960870f0-462b-4e89-b454-424d7d89c6b1" />

## Bước 2 : Tạo Project
### 1. Tạo Project - Class Library (DLL) — PikachuLibrary
**Trong Solution Explorer, click phải vào Solution : Pikachu → Add → New Project.**
+ Gõ tìm: Class Library (.NET Framework).
+ Chọn → Next.
+ Đặt tên project: Pikachu.
+ Ở phần Framework → chọn .NET Framework 2.0 → Create.
<img width="1122" height="982" alt="Screenshot 2025-09-26 212045" src="https://github.com/user-attachments/assets/143d4846-3e94-4920-83c5-153895007c2e" />

DLL này sẽ được dùng lại trong Console App, WinForm, và Web.  
DLL không có Main(), chỉ có class + method để người khác gọi.

#### Build & Run
+ Chuột phải vào PikachuLibrary
+ Build: Build → Build Solution
+ DLL không chạy trực tiếp. Nó chỉ được tham chiếu (reference) bởi các project khác.

<img width="1835" height="798" alt="Screenshot 2025-09-27 180732" src="https://github.com/user-attachments/assets/e859b8c5-d4ba-4523-a26b-464d8088540e" />

### 2. Tạo Project - Console App (.NET Framework 2.0)
+ Chuột phải vào Solution Pikachu → Add → New Project
+ Gõ tìm: Console App (.NET Framework) → Chọn .NET Framework 2.0.
+ Đặt tên: PikachuConsole  → Create.

<img width="1123" height="978" alt="Screenshot 2025-09-26 214730" src="https://github.com/user-attachments/assets/d21500ad-9f4b-4af7-9964-3ddeab24d1ec" />  

Sau khi tạo xong → References → Add Reference → Projects → Pikachu (DLL).

<img width="1001" height="683" alt="Screenshot 2025-09-27 181645" src="https://github.com/user-attachments/assets/7d2eafe7-9586-4817-b987-d14786fc2e47" />

### Thuật toán
#### 1. Input
+ Một bàn cờ kích thước rows × cols (ma trận 2D).
+ Mỗi ô có một số nguyên > 0 (đại diện cho hình Pikachu), hoặc 0 (ô trống).
+ Hai tọa độ (r1, c1) và (r2, c2) cần kiểm tra.

#### 2. Output
Trả về:
+ True (có thể nối được) hoặc False (không thể nối).
+ Trong trò chơi, nếu nối được thì xóa 2 ô (gán bằng 0).

#### 3. Thuật toán (CanConnect)
+ Nếu 2 ô không cùng giá trị hoặc đã rỗng → return False.
+ Dùng thuật toán BFS/DFS trên lưới để kiểm tra:
+ Có thể đi từ (r1, c1) tới (r2, c2) chỉ qua ô trống 0.
+ Số lần đổi hướng (turns) ≤ 2 (→ tức là 3 đoạn thẳng).
+ Nếu tìm được đường thỏa mãn → return True.

**Ví dụ :** Bàn cờ 5x5  
|       | **0** | **1** | **2** | **3** | **4** |
| ----- | ----- | ----- | ----- | ----- | ----- |
| **0** | 1     | 2     | 3     | 2     | 1     |
| **1** | 4     | 5     | 1     | 5     | 4     |
| **2** | 2     | 3     | 0     | 3     | 2     |
| **3** | 4     | 5     | 1     | 5     | 4     |
| **4** | 1     | 2     | 3     | 2     | 1     |

Ta thấy số 1 ở vị trí (0,0) và só 1 ở vị trí (0,4) thoản mãn các điều kiện : 
+ 2 số giống nhau
+ Có thể nối dưới 3 đoạn thẳng
+ Các đoạn thẳng không cắt qua số khác

**Kết quả :**  
|       | **0** | **1** | **2** | **3** | **4** |
| ----- | ----- | ----- | ----- | ----- | ----- |
| **0** | 0     | 2     | 3     | 2     | 0     |
| **1** | 4     | 5     | 1     | 5     | 4     |
| **2** | 2     | 3     | 0     | 3     | 2     |
| **3** | 4     | 5     | 1     | 5     | 4     |
| **4** | 1     | 2     | 3     | 2     | 1     |

Hai số vừa nhập nối được và biến mất được thay thế bằng số 0 (trống)

#### Build & Run
**Có thể kết nối được**  

<img width="1878" height="945" alt="Screenshot 2025-09-27 183333" src="https://github.com/user-attachments/assets/ca8bbeaf-14e9-4432-afb4-23cd32dea6ce" />  

**Không thể kết nối được**  

<img width="1689" height="928" alt="Screenshot 2025-09-27 183516" src="https://github.com/user-attachments/assets/0191048c-a016-4414-8df5-6065871c1e14" />  

### Tạo Project – Windows Form Application (.NET Framework 2.0)
+ Chuột phải vào Solution Pikachu → Add → New Project
+ Gõ tìm: Windows Forms App (.NET Framework).
+ Chọn → Next.
+ Đặt tên project: PikachuWinform.
+ Ở phần Framework → chọn .NET Framework 2.0 → Create.  

<img width="1104" height="960" alt="Screenshot 2025-09-27 184102" src="https://github.com/user-attachments/assets/8d61762b-ffac-4c31-8518-776a3067b6ef" />  

Sau khi tạo xong → References → Add Reference → Projects → Pikachu (DLL).  

<img width="997" height="690" alt="Screenshot 2025-09-27 184126" src="https://github.com/user-attachments/assets/1cb2f397-8d43-40c8-955d-65e6ba9c629f" />

#### Build & Run

### Tạo Project – ASP.NET Web Application (.NET Framework 2.0)
+ Chuột phải vào Solution Pikachu → Add → New Project
+ Tìm: ASP.NET Web Application (.NET Framework).
+ Đặt tên project: PikachuWebApp.
+ Ở phần framework chọn: .NET Framework 2.0 → Create.  

<img width="1147" height="1009" alt="Screenshot 2025-09-27 184331" src="https://github.com/user-attachments/assets/2dfdb563-e58a-4d41-9364-27f0396fa6b5" />  

Sau khi tạo xong tham chiếu đến DLL → References → Add Reference → Projects → Pikachu (DLL).  

<img width="997" height="681" alt="Screenshot 2025-09-27 184430" src="https://github.com/user-attachments/assets/b8fde24a-ceee-4c97-a7ae-22c48a613ec0" />  

#### Build & Run

Bước 7. Cấu hình IIS cho Project Web
Mở IIS Manager (bạn cần bật Windows feature “Internet Information Services”).
Thêm New Website:
Chọn đường dẫn đến thư mục YourNameWebApp.
Đặt Host name ví dụ yourname.local.
Chọn port 80 (nếu chưa dùng).
Mở file hosts (C:\Windows\System32\drivers\etc\hosts).
Thêm dòng:
127.0.0.1   yourname.local
Sau đó mở trình duyệt → gõ http://yourname.local/ để chạy web.





