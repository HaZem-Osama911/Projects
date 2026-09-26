<!DOCTYPE html>
<html lang="ar" dir="rtl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@yield('title', 'Thriving Together')</title>
    <link rel="icon" href="{{ asset('assets/image/logo.png') }}">
    <link rel="stylesheet" href="{{ asset('assets/css/all.min.css') }}">
    <link rel="stylesheet" href="{{ asset('assets/css/normalize.css') }}">
    <link rel="stylesheet" href="{{ asset('assets/css/home.css') }}">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Cairo:wght@200..1000&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Lalezar&family=Work+Sans:ital,wght@0,100..900;1,100..900&display=swap" rel="stylesheet">
    @stack('styles')
</head>
<body>
    <header class="header">
        <div class="container">
            <a href="{{ route('home') }}" class="logo">
                <img src="{{ asset('assets/image/logo.png') }}" alt="Thriving Together">
            </a>
            <input type="checkbox" id="check">
            <label for="check" class="check-list" aria-label="فتح قائمة التنقل">
                <i class="fa-solid fa-list"></i>
            </label>
            <ul class="main-nav">
                <li class="link"><a href="{{ route('home') }}">الصفحة الرئيسية</a></li>
                <li class="link"><a href="{{ route('article') }}">المقالات</a></li>
                <li class="link"><a href="{{ route('diagnose') }}">التشخيصات والعلاجات</a></li>
                <li class="link"><a href="{{ route('exam') }}">الاختبارات</a></li>
                <li class="link"><a href="{{ route('pronunciation') }}">تدريب النطق</a></li>
                <li class="nav-item"><a href="{{ route('home') }}#Contact" class="nav-link">مساعدة</a></li>

                @guest
                    <li class="link acc"><a href="{{ route('signup') }}">إنشاء حساب</a></li>
                    <li class="link"><a href="{{ route('login') }}">تسجيل الدخول</a></li>
                @else
                    <li class="link"><a href="{{ route('profile') }}">الملف الشخصي</a></li>
                    <li class="link">
                        <button type="submit" form="logout-form" class="nav-logout">تسجيل الخروج</button>
                    </li>
                @endguest
            </ul>
        </div>
    </header>

    @auth
        <form id="logout-form" action="{{ route('logout') }}" method="POST" hidden>
            @csrf
        </form>
    @endauth

    @if(session('success'))
        <div class="flash-message flash-success" role="status">
            {{ session('success') }}
        </div>
    @endif

    @if($errors->any())
        <div class="flash-message flash-error" role="alert">
            <ul>
                @foreach($errors->all() as $error)
                    <li>{{ $error }}</li>
                @endforeach
            </ul>
        </div>
    @endif

    <main class="page-content">
        @yield('content')
    </main>

    <footer class="footer">
        <div class="container">
            <div class="box">
                <h3 class="glow">Thriving Together</h3>
                <p class="text">نحن هنا من أجل أن ننهض سويًا.</p>
            </div>
            <div class="box">
                <ul class="links">
                    <li><a href="{{ route('home') }}">الرئيسية</a></li>
                    <li><a href="{{ route('article') }}">المقالات</a></li>
                    <li><a href="{{ route('diagnose') }}">التشخيصات والعلاجات</a></li>
                    <li><a href="{{ route('exam') }}">الاختبارات</a></li>
                </ul>
            </div>
            <div class="box footer-image">
                <img src="{{ asset('assets/image/1.png') }}" alt="">
            </div>
        </div>
        <p class="copyright">&copy; {{ now()->year }} جميع الحقوق محفوظة</p>
    </footer>

    @stack('scripts')
</body>
</html>
