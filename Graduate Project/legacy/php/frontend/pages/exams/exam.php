<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>الاختبارات</title>
    <link rel="shortcut icon" type="x-icon" href="../../assets/image/logo.png">
    <link rel="stylesheet" href="../../assets/css/home.css">
    <link rel="stylesheet" href="../../assets/css/exam.css">
    <link rel="stylesheet" href="../../assets/css/all.min.css">
    <link rel="stylesheet" href="../../assets/css/normalize.css">
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
            <a href="../../../backend/controllers/home.php" class="logo"><img src="../../assets/image/logo.png" alt="logo"></a>
            <input type="checkbox" id="check">
            <label for="check" class="check-list">
                <i class="fa-solid fa-list"></i>
            </label>
            <ul class="main-nav">
                <li class="link"><a href="../../../backend/controllers/home.php" target="_blank">الصفحة الرئيسية</a></li>
                <li class="link"><a href="../../../backend/controllers/article.php" target="_blank">المقالات</a></li>
                <li class="link"><a href="../../../backend/controllers/diagnose.php" target="_blank">التشخيصات والعلاجات</a></li>
                <li class="link"><a href="../../../backend/controllers/exam.php" target="_blank">الاختبارات</a></li>
                <li class="nav-item"><a href="#Contact" class="nav-link">مساعدة</a></li>
                <li class="link"> <a href=""></a> </li>
                <li class="link acc"><a href="../auth/signup.php" target="_blank" class="active">إنشاء حساب</a></li>
                <li class="link"><a href="../auth/login.php" target="_blank">تسجيل الدخول</a></li>
            </ul>
            <div class="profile">
                <input type="checkbox" id="toggle-menu">
                <label for="toggle-menu">
                    <i class="fa-solid fa-user"></i>
                </label>
                <div class="menu" id="submenu">
                    <div class="sub-menu">
                        <div class="sub-menu-info">
                            <i class="fa-solid fa-user"></i>
                            <h3>Name</h3>
                        </div>
                        <hr>
                        <a href="#" class="sub-menu-link">
                            <i class="fa-solid fa-user-pen"></i>
                            <p>Edit profile</p>
                            <span>></span>
                        </a>
                        <a href="#" class="sub-menu-link">
                            <i class="fa-solid fa-gear"></i>
                            <p>Settings & Privacy</p>
                            <span>></span>
                        </a>
                        <a href="#" class="sub-menu-link">
                            <i class="fa-solid fa-circle-question"></i>
                            <p>Help & Support</p>
                            <span>></span>
                        </a>
                        <a href="#" class="sub-menu-link">
                            <i class="fa-solid fa-right-from-bracket"></i>
                            <p>Logout</p>
                            <span>></span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="exercise-content">
        <div class="container">
            <div class="main-title">الاختبارات <span></span></div>
            <p class="description">اختار من بين مجموعة اختبارات دقيقة لمساعدتك على فهم حالة طفلك بشكل أفضل</p>
            <div class="guided-test-box">
                <h4>مش عارف تبدأ منين؟</h4>
                <p>جاوب على ٤ أسئلة بسيطة واحنا هنرشحلك أنسب اختبار لحالة طفلك </p>
                <a href="guidedexam.php" class="guided-link">ابدأ الاختبار</a>
            </div>
            <div class="grid">
                <div class="card intelligence">
                    <h3>اختبار الذكاء</h3>
                    <p>يساعد في تقييم مستوى التفكير والتركيز لدى الطفل</p>
                    <div class="a-container">
                        <a href="exam1.php">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class="card age">
                    <h3>قياس العمر العقلي</h3>
                    <p>يُستخدم لتحديد العمر العقلي الحقيقي للطفل مقارنة بعمره الزمني</p>
                    <div class="a-container">
                        <a href="exam2.php">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class="card language">
                    <h3>اختبار الفهم اللغوي</h3>
                    <p>يُقيّم قدرة الطفل على فهم واستيعاب اللغة</p>
                    <div class="a-container">
                        <a href="exam3.php">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class="card autism">
                    <h3>اختبار M-CHAT</h3>
                    <p>استبيان توعوي أولي لبعض مؤشرات التواصل، وليس تشخيصًا طبيًا</p>
                    <div class="a-container">
                        <a href="exam4.php">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class=" card char">
                    <h3>اختبار نطق الحروف</h3>
                    <p>يُقيّم قدرة الطفل علي نطق الحرف باستخدام AI </p>
                    <div class="a-container">
                        <a href="Speech_Project/language_selection.php">ابدأ الاختبار</a>
                    </div>
                </div>
            </div>
        </div> 
        <br>
        <br>
        <br>
        <br>
        <p class="descriptionn">
            ملحوظه مهمه :الأخصائي هو من يتم تشخيص الحالة وفقا لدراسه الحالة والملاحظة الدقيقة ,ثم يضع خطة فرديه
            مناسبة لكل حالة يحدد المدة المخصصة لكل برنامج و ينتقل من كل
            مرحله لاخري حسب تقدم الحاله<br> ويكون عنده معرفه بالبرامج الخاصه بكل حاله و لابد ان يتمتع الاخصائي بالمرونه
            في تعديل خطوات البرنامج الخاص بالحاله عند ظهور اي سلوك معطل أو تقدم مفاجئ
        </p>
    </div>

    </section>
    
<?php require_once $level . 'frontend/partials/footer.php'; ?>
