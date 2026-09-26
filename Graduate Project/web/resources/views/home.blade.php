@extends('layouts.app')
@section('title', 'الصفحة الرئيسية')

@section('content')
<div class="container">
        <div class="slider">
            <div class="list">
                <div class="item">
                    <img src="{{ asset('assets/image/slid1.jpg') }}" alt="">
                    <div class="content">
                        <div class="title">مرحبا بك في <span class="glow">Thriving Together</span></div>
                        <div class="type">ستكون معلم لطفلك</div>
                        <div class="description">
                            صنع خصيصا لمساعدة الاباء الذين لديهم اطفال يعانون من اضطراب تواصل
                            يعطيك بعض التمارين العلاجيه لمساعدة في النهوض بحاله الطفل بجانب مراكز التخاطب
                        </div>
                        <div class="button">
                            <button>المزيد</button>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <img src="{{ asset('assets/image/slid4.jpg') }}" alt="">
                    <div class="content">
                        <div class="title">مرحبا بك في <span class="glow">Thriving Together</span></div>
                        <div class="type">ساعد طفلك يتواصل أفضل </div>
                        <div class="description">

        يمكنك الاستعانه بصفحه الاختبارات للمساعدة في تشخيص الحاله ... ملحوظه مهمه :الأخصائي هو من يتم تشخيص الحالة وفقا لدراسه
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
                    <img src="{{ asset('assets/image/slid2.jpg') }}" alt="">
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
                    <img src="{{ asset('assets/image/slid3.jpg') }}">
                    <div class=" content">
                        <div class="title">مرحبا بك في <span class="glow">Thriving Together</span></div>
                        <div class="type">أول منصة تفاعلية تدعم اضطرابات التواصل من المنزل وبمساعدة مختصين</div>
                        <div class="description">
                            نساعدك عن طريق تطبيق باستخدام الذكاء الاصطناعي يساعد طفلك في النطق , المزيد من التمارين
                            بمساعدة مختصيين يمكنك اكتشاف المزيد عند تجربتك
                        </div>
                        <div class="button">
                            <button>المزيد</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="thumbnail">
                <div class="item">
                    <img src="{{ asset('assets/image/slid1.jpg') }}" alt="">
                </div>
                <div class="item">
                    <img src="{{ asset('assets/image/slid2.jpg') }}" alt="">
                </div>
                <div class="item">
                    <img src="{{ asset('assets/image/slid3.jpg') }}" alt="">
                </div>
                <div class="item">
                    <img src="{{ asset('assets/image/slid4.jpg') }}" alt="">
                </div>
            </div>
            <div class="nextPrevArrows">
                <button class="prev">
                    < </button>
                        <button class="next"> > </button>
            </div>
        </div>
    </div>
    <div id="services">
        <div class="services">
            <div class="main-title">خدماتنا<span></span></div>
            <div class="container">
                <div class="box">
                    <i class="fa-solid fa-newspaper"></i>
                    <h3>مقالات</h3>
                </div>
                <div class="box">
                    <i class="fa-duotone fa-regular fa-stethoscope"></i>
                    <h3>التشخيصات والعلاجات</h3>
                </div>
                <div class="box">
                    <i class="fa-brands fa-readme"></i>
                    <h3>اختبارات</h3>
                </div>
                <div class="box">
                    <i class="fa-solid fa-video"></i>
                    <h3>فيديوهات علاجيه</h3>
                </div>
                <div class="box">
                    <i class="fa-solid fa-comments"></i>
                    <h3>مساعدة</h3>
                </div>
            </div>
        </div>
    </div>
    <div class="about">
        <div id="about">
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
                        تحسين قدرته على التواصل والتواصل مع المجتمع بجانب مراكز التخاطب .
                    </p>
                </div>
                <img src="{{ asset('assets/image/autism1.png') }}" alt="" class="image">
            </div>
        </div>
    </div>
    <div class="cta-section">
        <div class="cta-container">
            <h2>ساعد طفلك يتواصل أفضل </h2>
            <p>.لأن كل كلمة من طفلك تستحق تُسمع , نساعدك في رحلتك مع طفلك مع تطبيق صنع خصيصًا له</p>
            <a href="{{ route('exam') }}" class="cta-button">ابدأ الآن</a>
        </div>
    </div>
    <section class="faq-section" id="faq">
        <h2 class="faq-title">الأسئلة الشائعة</h2>
        <div class="faq-container">

            <div class="faq-item">
                <button class="faq-question">إزاي أبدأ أستخدم الموقع مع طفلي؟</button>
                <div class="faq-answer">
                    <p>هتلاقي خطوات واضحة بتبدأ بتقييم حالة الطفل ثم ترشيحات للتمارين المناسبة لحالته.</p>
                </div>
            </div>

            <div class="faq-item">
                <button class="faq-question">هل الموقع مناسب لكل الأعمار؟</button>
                <div class="faq-answer">
                    <p>طبعًا، المحتوى بيتدرج حسب عمر الطفل وقدراته، من 3 سنين لحد 18 سنة.</p>
                </div>
            </div>

            <div class="faq-item">
                <button class="faq-question">هل التمارين دي بديل لمراكز التخاطب؟</button>
                <div class="faq-answer">
                    <p>مش بديل كامل، لكنها مكملة ممتازة، وبعض مراكز التخاطب بدأت تستخدم أدوات الموقع فعليًا.</p>
                </div>
            </div>

            <div class="faq-item">
                <button class="faq-question">هل في متخصصين بيراجعوا المحتوى؟</button>
                <div class="faq-answer">
                    <p>آه، في فريق من خبراء التخاطب راجعوا التمارين والمحتوى وبيتم تحديثهم باستمرار.</p>
                </div>
            </div>

            <div class="faq-item">
                <button class="faq-question">هل الموقع مجاني؟</button>
                <div class="faq-answer">
                    <p>آه، الموقع حاليًا مجاني بالكامل، وهدفه إنه يوصل الدعم لكل طفل محتاجه.</p>
                </div>
            </div>

        </div>
    </section>

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
                        <span>Email:</span> <a href="mailto:info@thriving.com">thrivingtogether@gmail.com</a>
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
                <form action="{{ route('contact.post') }}" method="post">
                    @csrf
                    <h2 class="form-title">Send us a message</h2>
                    <div class="form-fields">
                        <div class="form-group">
                            <input type="text" name="first_name" class="fname" placeholder="First Name" required>
                        </div>
                        <div class="form-group">
                            <input type="text" name="last_name" class="lname" placeholder="Last Name" required>
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
@endsection

@push('scripts')
<script src="{{ asset('assets/js/home.js') }}" defer></script>
@endpush
