<?php

namespace Tests\Feature;

use App\Models\ContactMessage;
use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Illuminate\Support\Facades\Hash;
use Tests\TestCase;

class ApplicationTest extends TestCase
{
    use RefreshDatabase;

    public function test_public_pages_render_successfully(): void
    {
        $routes = [
            'home',
            'article',
            'diagnose',
            'conditions.index',
            'exam',
            'exam.exam1',
            'exam.exam2',
            'exam.exam3',
            'exam.exam4',
            'exam.guidedexam',
            'diagnose.aphasia',
            'diagnose.autism',
            'diagnose.delay',
            'diagnose.fast_speech',
            'diagnose.hoarseness',
            'diagnose.hysterical_aphasia',
            'diagnose.lisping',
            'diagnose.mutism',
            'diagnose.nasality',
            'diagnose.stuttering',
            'diagnose.vocal_asthenia',
            'pronunciation',
            'login',
            'signup',
        ];

        foreach ($routes as $route) {
            $this->get(route($route))
                ->assertOk()
                ->assertHeader('X-Content-Type-Options', 'nosniff')
                ->assertHeader('X-Frame-Options', 'SAMEORIGIN')
                ->assertHeader('Referrer-Policy', 'strict-origin-when-cross-origin')
                ->assertHeader('Permissions-Policy', 'camera=(), geolocation=(), microphone=()');
        }
    }

    public function test_signup_creates_a_hashed_user_and_authenticates_them(): void
    {
        $response = $this->post(route('signup.post'), [
            'username' => 'Hazem',
            'email' => 'HAZEM@example.com',
            'password' => 'secure123',
            'password_confirmation' => 'secure123',
            'mobile' => '01012345678',
        ]);

        $response->assertRedirect(route('profile'));
        $this->assertAuthenticated();

        $user = User::firstOrFail();
        $this->assertSame('hazem@example.com', $user->email);
        $this->assertTrue(Hash::check('secure123', $user->password));
    }

    public function test_signup_rejects_duplicate_email_and_weak_password(): void
    {
        User::create([
            'username' => 'Existing',
            'email' => 'existing@example.com',
            'password' => 'secure123',
            'Mobile' => '01012345678',
        ]);

        $response = $this->from(route('signup'))->post(route('signup.post'), [
            'username' => 'Duplicate',
            'email' => 'EXISTING@example.com',
            'password' => 'short',
            'password_confirmation' => 'short',
            'mobile' => 'not-a-phone',
        ]);

        $response->assertRedirect(route('signup'));
        $response->assertSessionHasErrors(['email', 'password', 'mobile']);
        $this->assertDatabaseCount('yassdb', 1);
    }

    public function test_login_and_logout_flow(): void
    {
        $user = User::create([
            'username' => 'Hazem',
            'email' => 'hazem@example.com',
            'password' => 'secure123',
            'Mobile' => '01012345678',
        ]);

        $this->post(route('login.post'), [
            'email' => 'HAZEM@example.com',
            'password' => 'secure123',
            'remember' => '1',
        ])->assertRedirect(route('profile'));

        $this->assertAuthenticatedAs($user);
        $this->post(route('logout'))->assertRedirect(route('home'));
        $this->assertGuest();
    }

    public function test_contact_form_validates_and_persists_messages(): void
    {
        $response = $this->from(route('home'))->post(route('contact.post'), [
            'first_name' => 'Hazem',
            'last_name' => 'Test',
            'email' => 'hazem@example.com',
            'phone' => '+201012345678',
            'message' => 'رسالة اختبار',
        ]);

        $response->assertRedirect(route('home'));
        $response->assertSessionHas('success');
        $this->assertDatabaseCount('contact_messages', 1);
        $this->assertSame('رسالة اختبار', ContactMessage::firstOrFail()->message);
    }

    public function test_authenticated_user_can_update_profile(): void
    {
        $user = User::create([
            'username' => 'Old Name',
            'email' => 'old@example.com',
            'password' => 'secure123',
            'Mobile' => '01012345678',
        ]);

        $response = $this->actingAs($user)->put(route('profile.update'), [
            'username' => 'New Name',
            'email' => 'NEW@example.com',
            'mobile' => '01112345678',
        ]);

        $response->assertSessionHasNoErrors();
        $this->assertDatabaseHas('yassdb', [
            'id' => $user->id,
            'username' => 'New Name',
            'email' => 'new@example.com',
            'Mobile' => '01112345678',
        ]);
    }

    public function test_guests_cannot_access_profile(): void
    {
        $this->get(route('profile'))->assertRedirect(route('login'));
    }
}
