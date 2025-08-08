<?php
session_start();
$isLoggedIn = isset($_SESSION["loggeduser"]);
$username = $isLoggedIn ? $_SESSION["username"] : '';
?>
<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>الصفحه الرئيسيه</title>
    <link rel="shortcut icon" type="x-icon" href="image/logo.png">
    <link rel="stylesheet" href="css/home.css">
    <link rel="stylesheet" href="css/all.min.css">
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="preconnect" href="https:fonts.googleapis.com">
    <link rel="preconnect" href="https:fonts.gstatic.com" crossorigin>
    <link href="https:fonts.googleapis.com/css2?family=Cairo:wght@200..1000&display=swap" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link
        href="https://fonts.googleapis.com/css2?family=Lalezar&family=Work+Sans:ital,wght@0,100..900;1,100..900&display=swap"
        rel="stylesheet">
</head>

<body>
    <div class="header">
        <div class="container">
            <a href="home.php" class="logo"><img src="image/logo.png" alt="logo"></a>
            <input type="checkbox" id="check">
            <label for="check" class="check-list">
                <i class="fa-solid fa-list"></i>
            </label>
            <ul class="main-nav">
                <li class="link"><a href="home.php">الصفحه الرئيسيه</a></li>
                <li class="link"><a href="artical.php">المقالات</a></li>
                <li class="link"><a href="diagnose.php">التشخيصات والعلاجات</a></li>
                <li class="link"><a href="exam.php">الاختبارات</a></li>
                <li class="nav-item"><a href="#Contact" class="nav-link">مساعده</a></li>
                <li class="link"> <a href=""></a></li>

                <?php if($isLoggedIn): ?>
                <li class="link"><a href="profile.php">الملف الشخصي</a></li>
                <li class="link"><a href="logout.php">تسجيل الخروج</a></li>
                <?php else: ?>
                <li class="link acc"><a href="sighnup.html" class="active">انشاء حساب</a></li>
                <li class="link"><a href="login.html">تسجيل الدخول</a></li>
                <?php endif; ?>
            </ul>

            <?php if($isLoggedIn): ?>
            <div class="profile">
                <i class="fa-solid fa-user"></i>
            </div>
            <div class="menu" id="submenu">
                <div class="sub-menu">
                    <div class="sub-menu-info">
                        <i class="fa-solid fa-user"></i>
                        <h3><?php echo htmlspecialchars($username); ?></h3>
                    </div>
                    <hr>
                    <a href="profile.php" class="sub-menu-link">
                        <i class="fa-solid fa-user-pen"></i>
                        <p>الملف الشخصي</p>
                        <span>></span>
                    </a>
                    <a href="#" class="sub-menu-link">
                        <i class="fa-solid fa-gear"></i>
                        <p>الإعدادات</p>
                        <span>></span>
                    </a>
                    <a href="#Contact" class="sub-menu-link">
                        <i class="fa-solid fa-circle-question"></i>
                        <p>المساعدة</p>
                        <span>></span>
                    </a>
                    <a href="logout.php" class="sub-menu-link">
                        <i class="fa-solid fa-right-from-bracket"></i>
                        <p>تسجيل الخروج</p>
                        <span>></span>
                    </a>
                </div>
            </div>
            <?php endif; ?>
        </div>
    </div>

    <div class="containar">
        <div class="slider">
            <div class="list">
                <div class="item">
                    <img src="image/slid1.jpg" alt="">
                    <div class="content">
                        <div class="title">مرحبا بك في <span class="glow">Thriving Together</span></div>
                        <div class="type">
                            <?php if($isLoggedIn): ?>
                            أهلاً بك <?php echo htmlspecialchars($username); ?>
                            <?php else: ?>
                            ستكون معلم لطفلك
                            <?php endif; ?>
                        </div>
                        <div class="description">
                            صنع خصيصا لمساعده الاباء الذين لديهم اطفال يعانون من اضطراب تواصل
                            يعطيك بعض التمارين العلاجيه لمساعده في النهوض بحاله الطفل بجانب مراكز التخاطب
                        </div>
                        <div class="button">
                            <button>المزيد</button>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <img src="image/slid4.jpg" alt="">
                    <div class="content">
                        <div class="title">مرحبا بك في <span class="glow">Thriving Together</span></div>
                        <div class="type">
                            <?php if($isLoggedIn): ?>
                            أهلاً بك <?php echo htmlspecialchars($username); ?>
                            <?php else: ?>
                                ساعد طفلك بتواصل افضل
                            <?php endif; ?>
                        </div>
                        <div class="description">
                            يمكنك الاستعانه بصفحه الاختبارات للمساعده في تشخيص الحاله ... ملحوظه مهمه :الأخصائي هو من يتم تشخيص الحالة وفقا لدراسه
                            الحالة والملاحظة الدقيقة ,ثم يضع خطة فرديه مناسبة لكل حالة يحدد المدة المخصصة لكل برنامج و ينتقل من كل مرحله
                            لاخري حسب تقدم الحاله ويكون عنده معرفه بالبرامج الخاصه بكل حاله و لابد ان يتمتع الاخصائي بالمرونه في تعديل
                            خطوات البرنامج الخاص بالحاله عند ظهور اي سلوك معطل أو تقدم مفاجئ
                        </div>
                        <div class="button">
                            <button>المزيد</button>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <img src="image/slid2.jpg" alt="">
                    <div class="content">
                        <div class="title">مرحبا بك في <span class="glow">Thriving Together</span></div>
                        <div class="type">صدق في طفلك علشان يصدق في نفسه</div>
                        <div class="description">
                            بنساعدك انك تصدق في طفلك وتفهم حالته عن طريق الاختبارات الي بيها تقدر تفهم حاله طفلك وتساعده
                            عن طريق الفيديوهات والتمارين المتاحه علي الموقع
                        </div>
                        <div class="button">
                            <button>المزيد</button>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <img src="image/slid3.jpg">
                    <div class=" content">
                        <div class="title">مرحبا بك في <span class="glow">Thriving Together</span></div>
                        <div class="type">أول منصة تفاعلية تدعم اضطرابات التواصل من المنزل وبمساعدة مختصين</div>
                        <div class="description">
                            نساعدك عن طريق تطبيق باستخدام الذكاء الاصطناعي يساعد طفلك في النطق , المزيد من التمارين
                            بمساعده مختصيين يمكنك اكتشاف المزيد عند تجربتك
                        </div>
                        <div class="button">
                            <button>المزيد</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="thumbnail">
                <div class="item">
                    <img src="image/slid1.jpg" alt="">
                </div>
                <div class="item">
                    <img src="image/slid2.jpg" alt="">
                </div>
                <div class="item">
                    <img src="image/slid3.jpg" alt="">
                </div>
                <div class="item">
                    <img src="image/slid4.jpg" alt="">
                </div>
            </div>
            <div class="nextPrevArrows">
                <button class="prev">
                    < </button>
                        <button class="next"> > </button>
            </div>
        </div>
    <div id="services">
        <div class="services">
            <div class="main-title">الخدمات<span></span></div>
            <div class="container">
                <div class="box">
                    <i class="fa-solid fa-newspaper"></i>
                    <h3>المقالات</h3>
                </div>
                <div class="box">
                    <i class="fa-duotone fa-regular fa-stethoscope"></i>
                    <h3>التشخيصات والعلاجات</h3>
                </div>
                <div class="box">
                    <i class="fa-brands fa-readme"></i>
                    <h3>الاختبارات</h3>
                </div>
                <div class="box">
                    <i class="fa-solid fa-comments"></i>
                    <h3>المساعدة</h3>
                </div>
            </div>
        </div>
    </div>
    <div id="about">
        <div class="about">
            <h2 class="main-title">المزيد عننا<span></span></h2>
            <div class="container">
                <div class="box">
                    <p>
                        الأطفال مثل الملائكة على الأرض، يمثلون أجمل جزء في حياة أي والد.
                        في حين أن الأطفال جزء لا يتجزأ من المجتمع، يواجه البعض تحديات في التواصل - سواء كان لفظيًا أو
                        غير
                        لفظي.
                        في عالم اليوم، هناك وعي متزايد بالقضايا التي يواجهها هؤلاء الأطفال، وقد زاد توافر المتخصصين
                        والمراكز
                        العلاجية بشكل كبير.
                        لدعم الآباء في هذه الرحلة، نقدم "Thriving Together"، وهو موقع ويب مصمم لإرشادك في فهم طفلك
                        ومساعدته على
                        تحسين قدرته على التواصل والتواصل مع المجتمع.
                    </p>
                </div>
                <img src="image/autism1.png" alt="" class="image">
            </div>
        </div>
    </div>
    <div class="contact-us" id="Contact">
        <div class="container">
            <div class="info-wrap">
                <h2 class="info-title">Contact Information</h2>
                <h3 class="info-sub-title">Fill up the form and our Team will get back to you within 24 hours</h3>
                <ul class="info-details">
                    <li>
                        <i class="fas fa-phone-alt"></i>
                        <span>Phone:</span> <a href="tel:+ 1235 2355 98">+ 1235 2355 98</a>
                    </li>
                    <li>
                        <i class="fas fa-paper-plane"></i>
                        <span>Email:</span> <a href="mailto:info@thriving.com">info@yoursite.com</a>
                    </li>
                    <li>
                        <i class="fas fa-globe"></i>
                        <span>Website:</span> <a href="#">Thriving Together</a>
                    </li>
                </ul>
                <ul class="social-icons">
                    <li>
                        <a href="#" class="facebook">
                            <i class="fab fa-facebook"></i>
                        </a>
                    </li>
                    <li>
                        <a href="#" class="twitter">
                            <i class="fab fa-twitter"></i>
                        </a>
                    </li>
                    <li>
                        <a href="#" class="linkedin">
                            <i class="fab fa-linkedin-in"></i>
                        </a>
                    </li>
                </ul>
            </div>
            <div class="form-wrap">
                <form action="contact.php" method="post">
                    <h2 class="form-title">Send us a message</h2>
                    <div class="form-fields">
                        <div class="form-group">
                            <input type="text" name="FirstName" class="fname" placeholder="First Name" required>
                        </div>
                        <div class="form-group">
                            <input type="text" name="LastName" class="lname" placeholder="Last Name" required>
                        </div>
                        <div class="form-group">
                            <input type="email" name="email" class="email" placeholder="Mail" required>
                        </div>
                        <div class="form-group">
                            <input type="tel" name="phone" class="phone" placeholder="Phone" required>
                        </div>
                        <div class="form-group">
                            <textarea name="message" placeholder="Write your message" required></textarea>
                        </div>
                    </div>
                    <input type="submit" value="Send Message" class="submit-button">
                </form>
            </div>
        </div>
    </div>
    <div class="footer">
        <div class="container">
            <div class="box">
                <h3>Thriving Together</h3>
                <ul class="social">
                    <li>
                        <a href="#" class="facebook">
                            <i class="fab fa-facebook-f"></i>
                        </a>
                    </li>
                    <li>
                        <a href="#" class="twitter">
                            <i class="fab fa-twitter"></i>
                        </a>
                    </li>
                    <li>
                        <a href="#" class="email">
                            <i class="fa-solid fa-envelope"></i>
                        </a>
                    </li>
                </ul>
                <p class="text">
                    "نحن هنا من أجل ان ننهض سويا"
                </p>
            </div>
            <div class="box">
                <ul class="links">
                    <li><a href="home.php">الرئيسيه</a></li>
                    <?php if(!$isLoggedIn): ?>
                    <li><a href="sighnup.html">انشاء حساب</a></li>
                    <li><a href="login.html">تسجيل الدخول</a></li>
                    <?php else: ?>
                    <li><a href="profile.php">الملف الشخصي</a></li>
                    <li><a href="logout.php">تسجيل الخروج</a></li>
                    <?php endif; ?>
                    <li><a href="artical.php">المقالات</a></li>
                    <li><a href="diagnose.php">تشخيصات وعلاجات</a></li>
                    <li><a href="exam.php">الاختبارات</a></li>
                </ul>
            </div>
            <div class="box footer-image">
                <img src="image/1.png" alt="">
            </div>
        </div>
        <p class="copyright">&copy;All Rights Reserved</p>
    </div>

    <script>
        let submenu = document.getElementById("submenu");
        function togglemenu() {
            submenu.classList.toggle("open-menu");
        }
        const faqItems = document.querySelectorAll('.faq-item');

        faqItems.forEach(item => {
            const question = item.querySelector('.faq-question');
            const answer = item.querySelector('.faq-answer');

            question.addEventListener('click', () => {
                const isActive = question.classList.contains('active');

                // اقفل الكل الأول
                document.querySelectorAll('.faq-answer').forEach(a => a.style.maxHeight = null);
                document.querySelectorAll('.faq-question').forEach(q => q.classList.remove('active'));

                if (!isActive) {
                    question.classList.add('active');
                    answer.style.maxHeight = answer.scrollHeight + 'px';
                }
            });
        });
    </script>
    <script src="js/home.js"></script>
</body>

</html>