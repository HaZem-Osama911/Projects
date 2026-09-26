<?php

namespace App\Http\Controllers;

use Illuminate\Contracts\View\View;
use Illuminate\Http\RedirectResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Str;
use Illuminate\Validation\Rule;

class ProfileController extends Controller
{
    public function show(Request $request): View
    {
        return view('dashboard.profile', ['user' => $request->user()]);
    }

    public function update(Request $request): RedirectResponse
    {
        $user = $request->user();

        $request->merge([
            'email' => Str::lower(trim((string) $request->input('email'))),
        ]);

        $validated = $request->validate([
            'username' => ['required', 'string', 'max:50'],
            'email' => [
                'required',
                'email',
                'max:255',
                Rule::unique('yassdb', 'email')->ignore($user->getKey()),
            ],
            'mobile' => ['required', 'regex:/^\+?[0-9]{8,15}$/'],
        ]);

        $user->update([
            'username' => $validated['username'],
            'email' => $validated['email'],
            'Mobile' => $validated['mobile'],
        ]);

        return back()->with('success', 'تم تحديث بياناتك بنجاح.');
    }
}
