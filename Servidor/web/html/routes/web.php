<?php

use App\Usuario;
use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| Application Routes
|--------------------------------------------------------------------------
|
| Here is where you can register all of the routes for an application.
| It is a breeze. Simply tell Lumen the URIs it should respond to
| and give it the Closure to call when that URI is requested.
|
*/
$app->get('/', function () use ($app) {
    return 'Welcome to MadreApp API';
});

$app->group(['prefix' => 'api'], function () use ($app) {
	//List all ligacoes and paginate it
	$app->get('ligacao', 'LigacaoController@index');
	$app->post('ligacao', 'LigacaoController@store');
	$app->get('ligacao/refresh', 'LigacaoController@refresh');
	$app->get('ligacao/{id}', 'LigacaoController@index');
	$app->put('ligacao/{id}', 'LigacaoController@update');
	
	//Checa se o cpf tem madrecard
	$app->get('madrecard/{cpf}', function (Request $request, $cpf) {
		if(strlen($cpf) != 11) {
			return response()->json(array(
                    'code'      =>  400,
                    'message'   =>  'Usuário não possui MadreCard'
                ), 400);
		}
		return [
			'numero' => mt_rand(1000000000, 9999999999),
			'ativo' => true,
			'cpf' => $cpf,
			'nome' => 'Nome Ficticio '.mt_rand(1,100),
			'saldo' => mt_rand(25,500).','.mt_rand(0,99)
		];
    });
	
	$app->post('usuario', function (Request $request) {
		$this->validate($request, [
			'nome' => 'required|string',
			'telefone' => 'required|min:10',
			'email' => 'email',
            'cpf' => 'max:11|unique:usuarios,cpf',
		]);
		
		$usuario = new Usuario(['nome' => $request->nome, 
								'telefone' => $request->telefone,
								'email' => $request->has('email') ? $request->email : null,
								'cpf' => $request->has('cpf') ? $request->cpf : null]);
		$usuario->save();
		
		return $usuario;
    });	
	$app->put('usuario/{id}', function (Request $request, $id) {
           $validate = [
               'nome' => 'sometimes|required|string',
               'telefone' => 'sometimes|required|min:10',
               'email' => 'email'
           ];
	   if($request->has('cpf')) 
	   {
               $validate['cpf'] = 'max:11|unique:usuarios,cpf,'.$request->cpf;
           }
	   $this->validate($request, $validate);
		
	   $usuario = Usuario::findOrFail($id);
	   $data = array_filter($request->only(['nome', 'telefone', 'email', 'cpf']));
           $usuario->fill($data);
	   $usuario->save();
		
	   return $usuario;
    });
});

