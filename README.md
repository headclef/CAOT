<!DOCTYPE html>
<html lang="tr">
<head>
  <meta charset="UTF-8">
</head>
<body style="margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; font-size: 1.5rem; line-height: 1.6; background-color: #fff; color: #222;">
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h1 style="text-align: center; font-size: 4rem;">CAOT</h1>
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">PROJE AÇIKLAMASI</h3>
		<p style="text-align: justify; font-size: 1.75rem;">
			.NET 9.0 teknolojisi ile geliştirilmiş, sıfırdan proje geliştirme işlemlerinin önüne geçmek için toplamda beş, ter katmandan tasarlanmış, olabildiğince basitleştirilmiş bir proje taslağıdır.
		</p>
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">PROJE DURUMU</h3>
		<p style="text-align: justify; font-size: 1.75rem;">
			Taslak olarak yüksek oranda hizmet görebilecek durumdadır ancak henüz tamamlanmamıştır! <b>Kısacası henüz geliştirme aşamasındadır.</b>
		</p>
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">KURULUM TALİMATLARI</h3>
		<p style="text-align: justify; font-size: 1.75rem;">
			GitHub 'dan projeyi kurmak için aşağıdaki adımları izleyin:
		</p>
		<ol style="font-size: 1.75rem; padding-left: 40px;">
			<li>Terminal veya Komut İstemcisi'ni açın.</li>
			<li>Projeyi klonlayın: <pre><code>git clone https://github.com/headclef/CAOT.git</code></pre></li>
			<li>Proje dizinine gidin: <pre><code>cd CAOT</code></pre></li>
			<li>Bağımlılıkları yükleyin: <pre><code>dotnet restore</code></pre></li>
			<li>Projeyi çalıştırın: <pre><code>dotnet run</code></pre></li>
		</ol>
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">KULLANIM KILAVUZU</h3>
		<p style="text-align: justify; font-size: 1.75rem;">
			Geliştirecek olduğunuz projenin ön yüzünü ve arka yüzünü tanımlayabileceğiniz yapıyı elde etmiş olursunuz. Bu yapıyı kullanarak projenizi geliştirebilir ve yönetebilirsiniz. Dilerseniz proje yapısını değiştirebilir ve kendi projenize uygun hale getirebilirsiniz. Geliştirmelerinizi yaparken proje yapısına uyum sağlayarak ilerlerseniz, yüksek oranda temiz mimariyi koruyarak ilerleme şansına sahip olursunuz.
		</p>
	</div>
	<div style="max-width: 90%; margin: 30px auto; text-align: center;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">PROJE MİMARİSİ</h3>
		<p style="font-size: 1.75rem; text-align: justify;">
			Aşağıdaki diyagram, projenin katmanlı mimarisini (Onion Architecture) yansıtmaktadır. İçten dışa doğru bağımlılık akışını ve katmanların hiyerarşisini göstermektedir. Hiyerarşilerin detaylı açıklaması aşağıda yapılacaktır.
		</p>
		<img src="Template.png" alt="Soğan Mimarisi Diyagramı" style="max-width: 100%; height: auto; border: 1px solid #ccc; border-radius: 8px; margin-top: 10px;" />
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">PROJE KATMANLARI</h3>
		<ul style="font-size: 1.75rem; padding-left: 40px;">
			<li><strong>Core</strong>
				<ul>
					<li>Domain (Entity and Interface Layer)</li>
				</ul>
			</li>
			<li><strong>Infrastructure</strong>
				<ul>
					<li>Persistence (Context Layer)</li>
				</ul>
			</li>
			<li><strong>Business</strong>
				<ul>
					<li>Application (Service Layer)</li>
					<li>DTO (Data Transfer Object Layer)</li>
					<li>Mapper (Mapping Layer)</li>
				</ul
			</li>
			<li><strong>Web</strong>
				<ul>
					<li>API (Controller Layer)</li>
				</ul>
			</li>
			<li><strong>Presentation</strong>
				<ul>
					<li>MVC UI (User Interface Layer) - HENÜZ EKLENMEDİ</li>
				</ul>
			</li>
			<li><strong>Test</strong>
				<ul>
					<li>Unit Tests</li>
					<li>Fake Data</li>
					<li>Test Helpers</li>
				</ul>
			</li>
		</ul>
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">ÖZELLİKLER</h3>
		<ul style="font-size: 1.75rem; padding-left: 40px;">
			<li>Katmanlı mimari (Onion Architecture - Soğan Mimarisi)</li>
			<li>Entity Framework Core ile veri yönetimi</li>
			<li>Repository & Unit of Work pattern desteği</li>
			<li>AutoMapper ile model–entity dönüşümleri</li>
			<li>Merkezi loglama sistemi</li>
			<li>Exception Handling mekanizması</li>
			<li>Response Wrapper ile standart cevaplar</li>
			<li>SMTP destekli e-posta servisi</li>
			<li>Background mail queue (async işlem yapısı)</li>
			<li>Unit Test altyapısı (xUnit, Moq)</li>
		</ul>
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">LOGLAMA</h3>
		<p style="text-align: justify; font-size: 1.75rem;">
			Loglama işlemleri <code>Application</code> katmanında merkezi olarak gerçekleştirilir. Log türleri (Bilgi, Hata, Uyarı) için farklı dosyalar oluşturulur ve dosya boyutu ile tarihe göre yönetilir.
		</p>
		<pre style="padding: 10px; border-radius: 6px; overflow-x: auto; font-size: 1.5rem;">
private void PrepareLogEnvironment(string logType)
{
    EnsureLogPathExists();           // 1️⃣ Klasör oluşturulur
    EnsureLogFileExists(logType);    // 2️⃣ Dosya kontrolü yapılır
    RotateLogFile(logType);          // 3️⃣ Boyut/tarih kontrolü ile arşive taşınır
    ArchiveOldLogsToZip();           // 4️⃣ Arşiv klasörü ziplenir
}		</pre>
		<p style="text-align: justify; font-size: 1.75rem;">
			Bu yapı sayesinde gereksiz log birikimi önlenir, okunabilirlik ve yönetilebilirlik sağlanır. İlerleyen sürümlerde veritabanı loglama ya da dış servis entegrasyonları da yapılabilir.
		</p>
	</div>
	<div style="max-width: 90%; margin: 0 auto 40px auto;">
		<h3 style="text-align: center; font-size: 2.25rem; font-weight: bold;">KATKIDA BULUNANLAR</h3>
		<p style="text-align: justify; font-size: 1.75rem;">
			Bu projeye katkıda bulunanlar:
		</p>
		<ul style="font-size: 1.75rem; padding-left: 40px;">
			<li><a href="https://www.linkedin.com/in/furkantural/">Furkan Tural - Repo Sahibi</a></li>
			<li><a href="https://www.linkedin.com/in/mustafaurhan/">Mustafa Urhan</a></li>
			<li><a href="https://www.linkedin.com/in/m%C3%BCnir-soysal-b92b72240/">Musa Münir Soysal</a></li>
		</ul>
	</div>
</body>
</html>
