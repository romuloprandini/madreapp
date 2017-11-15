<?php

namespace App\Http\Controllers;

use App\Ligacao;
use Illuminate\Http\Request;

class LigacaoController extends Controller
{
    /**
     * Create a new controller instance.
     *
     * @return void
     */
    public function __construct() {
        //
    }
	
	public function index(Request $request) {
		if($request->has('search') && $request->search != null && trim($request->search) != '') {
			$ligacoes = Ligacao::where('telefone','like','%'.$request->search.'%')->orderBy('created_at','desc');
		} else {
			$ligacoes = Ligacao::orderBy('created_at','desc');
		}
		$per_page = 10;
		if($request->has('per_page')) {
			$per_page = $request->per_page;
		}
		return $ligacoes->paginate($per_page);
    }
	
	public function refresh(Request $request) {
		$this->validate($request, [
			'last_time' => 'required|numeric',
		]);
		$time = intval($request->last_time/1000);

		$conta_ligacoes = Ligacao::whereNull('retorno')
							 ->whereDate('created_at', '>=', date('Y-m-d', $time))
							 ->whereTime('created_at', '>', date('H:i:s', $time))
							 ->count();
							 
		return response()->json(['count' => $conta_ligacoes]);
	}
	
	public function show(IRequest $request, $id) {
		return Ligacao::findOrFail($id);
	}

	public function store(Request $request) {
		$this->validate($request, [
			'nome' => 'required',
			'telefone' => 'required|min:10',
		]);
		
		$ligacao = new Ligacao();
		$ligacao->telefone = $request->telefone;
		$ligacao->nome = $request->has('nome') ? $request->nome : null;
		$ligacao->cpf = $request->has('cpf') ? $request->cpf : null;
		
		if($request->has('madrecard')) {
			$ligacao->madrecard_numero = $request->madrecard['numero'];
			$ligacao->madrecard_ativo = $request->madrecard['ativo'];
		}
		
		$ligacao->save();
		
		return $ligacao;
	}
	
	public function update(Illuminate\Http\Request $request, $id) {
		$this->validate($request, [
			'retorno' => 'required',
		]);
		
		$ligacao = Ligacao::findOrFail($id);
		$ligacao->retorno = $request->retorno;
		$ligacao->save();
		
		return $ligacao;
	}
}
