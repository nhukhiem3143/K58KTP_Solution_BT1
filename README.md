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
## Bước 1: Tạo Solution
1. Mở Visual Studio 2022.
2. Chọn File → New → Project.
3. Tìm template Blank Solution (hoặc gõ từ khóa "Blank Solution" trong ô tìm kiếm).
4. Đặt tên Solution : Pikachu → Create.

<img width="1140" height="988" alt="Screenshot 2025-09-26 211714" src="https://github.com/user-attachments/assets/960870f0-462b-4e89-b454-424d7d89c6b1" />

### Bước 2: Tạo Project - Class Library (DLL) — PikachuLibrary
**Mục đích:** Tạo một DLL đa năng và độc lập chứa logic cốt lõi của game Pikachu. DLL này xử lý việc kiểm tra xem hai ô ảnh có thể ghép cặp được không (theo quy tắc đường đi thẳng hoặc rẽ tối đa 2 lần, không cắt qua ô khác), xóa cặp ảnh khi ghép đúng, và quản lý trạng thái bảng game. Nó nhận input (vị trí ô, dữ liệu bảng) qua thuộc tính và trả kết quả (có ghép được không, bảng mới) qua phương thức hoặc thuộc tính khác.  

**Ứng dụng:** DLL có thể được tái sử dụng trong các ứng dụng khác nhau (Console, Windows Form, Web) mà không cần thay đổi logic, đảm bảo tính linh hoạt và độc lập.  

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

### Bước 3: Tạo Project - Console App (.NET Framework 2.0)
**Mục đích:** Tạo một ứng dụng dòng lệnh để chơi game Pikachu một cách đơn giản. Người dùng nhập tọa độ hai ô cần ghép, chương trình gọi DLL PikachuLibrary để kiểm tra và hiển thị kết quả (thành công hoặc thất bại). Đây là cách kiểm tra nhanh logic game mà không cần giao diện đồ họa.  

**Ứng dụng:** Phù hợp cho việc debug hoặc chạy game trên terminal, đặc biệt hữu ích trong môi trường không có màn hình đồ họa.  

**Trong Solution Explorer chuột phải vào Solution Pikachu → Add → New Project**
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

### Bước 4: Tạo Project – Windows Form Application (.NET Framework 2.0)
**Mục đích:** Tạo một ứng dụng giao diện đồ họa với cửa sổ để chơi game Pikachu. Người dùng nhấp chuột chọn hai ô trên bảng, DLL PikachuLibrary xử lý logic ghép cặp, và kết quả (thành công/thất bại, cập nhật bảng) được hiển thị trực quan trên form. Đây là cách trải nghiệm game với giao diện thân thiện.  

**Ứng dụng:** Phù hợp cho người dùng muốn chơi game trên desktop với hiệu ứng hình ảnh (ví dụ: mờ ô khi chọn, xóa ô khi ghép đúng).  

**Trong Solution Explorer chuột phải vào Solution Pikachu → Add → New Project**
+ Gõ tìm: Windows Forms App (.NET Framework).
+ Chọn → Next.
+ Đặt tên project: PikachuWinform.
+ Ở phần Framework → chọn .NET Framework 2.0 → Create.  

<img width="1104" height="960" alt="Screenshot 2025-09-27 184102" src="https://github.com/user-attachments/assets/8d61762b-ffac-4c31-8518-776a3067b6ef" />  

Sau khi tạo xong → References → Add Reference → Projects → Pikachu (DLL).  

<img width="997" height="690" alt="Screenshot 2025-09-27 184126" src="https://github.com/user-attachments/assets/1cb2f397-8d43-40c8-955d-65e6ba9c629f" />

**Thiết kế giao diện:**
Mở form1.cs trong chế độ thiết kế (Design), thêm các control như TextBox (để nhập kích thước bảng), Button (để khởi tạo bảng), và một Panel hoặc PictureBox (để vẽ bảng game).
Sử dụng sự kiện chuột (MouseClick) trên Panel để chọn ô.  

<img width="1920" height="1080" alt="Screenshot 2025-09-27 201131" src="https://github.com/user-attachments/assets/2d1f6e4f-9c67-4ca8-9799-fb9a9d71e314" />

**Tích hợp ảnh với bảng game:**
1. Nhấp chuột phải vào project PikachuWinForm trong Solution Explorer → Properties.  
2. Chọn tab Resources (nếu chưa có, nhấp This project does not contain a default resources file. Click here to create one).  
3. Trong tab Resources, nhấp Add Resource → Add Existing File, duyệt đến nơi lưu các file ảnh.
  
<img width="1920" height="1080" alt="Screenshot 2025-09-27 202718" src="https://github.com/user-attachments/assets/04d29ca1-5f65-46fa-a1ea-c84ad308f663" />  

**Thiết kế luồng chương trình:**
1. Khi nhấn btnInitialize, gọi phương thức trong DLL để tạo bảng ngẫu nhiên (mảng 2D với giá trị từ 1 đến 30).
2. Gán ảnh cho từng PictureBox dựa trên giá trị ô:
  + Nếu ô có giá trị 1, tải ảnh pika1.png vào picBox_0_0.Image.
  + Nếu ô trống (giá trị 0), đặt picBox_0_0.Image = null và tô màu nền (ví dụ: xanh nhạt).

#### Build & Run
<img width="1920" height="1080" alt="Screenshot 2025-09-27 203950" src="https://github.com/user-attachments/assets/47236970-c653-4474-b3af-0c58109b8ed1" />  

### Bước 5: Tạo Project – ASP.NET Web Application (.NET Framework 2.0)
**Mục đích:** Tạo một ứng dụng web để chơi game Pikachu trực tuyến. Người dùng tương tác với giao diện HTML/CSS/JS để chọn ô, JavaScript gửi yêu cầu đến backend (api.aspx), backend gọi DLL PikachuLibrary để xử lý logic, và trả về JSON. JavaScript xử lý JSON để cập nhật giao diện (vẽ bảng, xóa ô). Đây là cách triển khai game trên nền web với server IIS.  

**Ứng dụng:** Cho phép chơi game qua trình duyệt, phù hợp với người dùng internet, và tận dụng IIS để host.  

**Trong Solution Explorer chuột phải vào Solution Pikachu → Add → New Project**
+ Tìm: ASP.NET Web Application (.NET Framework).
+ Đặt tên project: PikachuWebApp.
+ Ở phần framework chọn: .NET Framework 2.0 → Create.  

<img width="1147" height="1009" alt="Screenshot 2025-09-27 184331" src="https://github.com/user-attachments/assets/2dfdb563-e58a-4d41-9364-27f0396fa6b5" />  

Sau khi tạo xong tham chiếu đến DLL → References → Add Reference → Projects → Pikachu (DLL).  

<img width="997" height="681" alt="Screenshot 2025-09-27 184430" src="https://github.com/user-attachments/assets/b8fde24a-ceee-4c97-a7ae-22c48a613ec0" />  

**Thiết kế giao diện:**
Chuột phải PikachuWebApp → Chọn add → NewItem → Html
+ Tạo file index.html với HTML để hiển thị bảng game, CSS để định dạng (ví dụ: lưới ô, nút bắt đầu), và JavaScript để xử lý sự kiện nhấp chuột, gửi yêu cầu AJAX đến api.aspx.  
JavaScript vẽ bảng ban đầu, xử lý chọn ô (thêm hiệu ứng mờ), và cập nhật giao diện dựa trên JSON từ server.  

**Thiết kế backend:**
+ Tạo file api.aspx  
```<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="api.aspx.cs" Inherits="PikachuWebApp.Api" %>```  

sẽ tự động sinh ra file api.aspx.cs.  

+ Trong api.aspx.cs, nhận input (hành động như "generate" hoặc "check", tọa độ ô) từ JavaScript, gọi DLL PikachuLibrary để tạo bảng hoặc kiểm tra ghép cặp, và trả về JSON (thành công/thất bại, bảng mới).

#### Build & Run
<img width="1920" height="1080" alt="Screenshot 2025-09-27 205131" src="https://github.com/user-attachments/assets/de85eabf-bfe8-4a98-99e5-ce4bc7d25c21" />

### Bước 6: Cấu hình IIS cho Project Web
#### 1. Bật IIS trên Windows
1. Mở Control Panel → Programs and Features.
2. Ở sidebar → chọn Turn Windows features on or off (Bật/tắt tính năng Windows).
3. Nhấn chọn:
  + Internet Information Services
  + Trong đó bật Web Management Tools và World Wide Web Services.
  + Quan trọng: Trong Application Development Features, bật:
      + ASP.NET 4.8 (hoặc ASP.NET 3.5 nếu cài sẵn .NET 2.0)
      + ISAPI Extensions
      + ISAPI Filters  
Nhấn OK, đợi Windows cài đặt IIS.  
<img width="636" height="514" alt="image" src="https://github.com/user-attachments/assets/47722518-747a-40a2-a8a3-4a276411c04a" />  

#### 2. Tạo Website trong IIS
Mở IIS Manager:
Trong cây bên trái, chuột phải Sites → Add Website….
Nhập:
+ Site name: PikachuWebApp
+ Physical path: chọn folder chứa project web (chính là thư mục PikachuWebApp của solution).
+ Binding:
    + Type: http
    + IP address: All Unassigned
    + Port: 80.
    + Host name: pikachu.local
Nhấn OK.  

<img width="733" height="937" alt="Screenshot 2025-09-27 210313" src="https://github.com/user-attachments/assets/3b0d85b2-d677-4626-ae6c-e5424fe4d486" />

#### 3. Thêm domain vào file hosts
Mở Notepad → Run as Administrator.  
Mở file:  
```C:\Windows\System32\drivers\etc\hosts```  
Thêm dòng:  
```127.0.0.1   pikachu.local```  

<img width="808" height="167" alt="Screenshot 2025-09-27 211355" src="https://github.com/user-attachments/assets/13bc31c5-0380-42f9-8086-fb4766f98ec6" />  

#### 4. Cấu hình Application Pool
1. Trong IIS Manager → Application Pools.  
2. Tìm Application Pool vừa tạo PikachuWebApp.  
3. Nhấn chuột phải → Basic Settings…
+ .NET CLR version: chọn v2.0 (theo yêu cầu đề bài).
+ Managed pipeline mode: chọn Integrated.
Nhấn OK.
<img width="1206" height="619" alt="Screenshot 2025-09-27 210546" src="https://github.com/user-attachments/assets/de99fd64-5472-466c-b7c3-b74f031402fa" />  

#### 5. Copy DLL vào bin
Đảm bảo trong thư mục PikachuWebApp có folder bin/.
Bên trong phải có:
+ PikachuWebApp.dll (web project).
+ Pikachu.dll (DLL đa năng).
Nếu chưa có → Build Solution trong Visual Studio → tự động sinh bin/.  

<img width="810" height="302" alt="Screenshot 2025-09-27 210919" src="https://github.com/user-attachments/assets/9360d1c6-c670-49fe-8f5e-63c3a1140337" />  

Sau đó mở trình duyệt → gõ http://pikachu.local/ để chạy web.

# Kết quả 
<img width="1920" height="1080" alt="Screenshot 2025-09-27 211042" src="https://github.com/user-attachments/assets/01b5e5cd-af9f-456f-b220-3ad4484910c0" />  

# The end
