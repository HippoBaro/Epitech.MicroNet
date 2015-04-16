<h1 id="epitech-micronet">Epitech MicroNet</h1>

<p><strong>MicroNet</strong> est un client iOS (et bientôt Android !) permettant d’accéder à <a href="https://intra.epitech.eu/">l’Intranet de l’école Epitech</a>. Il est développé en C# et utilise le framework <a href="http://xamarin.com/platform">Xamarin</a> (anciennement Monotouch) pour générer du code natif. <br>
Le code utilise l’abstraction de Xamarin.Forms pour déclarer des interfaces <em>“Device agnostic”</em> : le même code compile pour Android, Windows Phone et iOS (voir OSX et Windows) en utilisant les API de chaque OS.</p>

<p><strong><em>Xamarin est gratuit pour les étudiants !</em></strong></p>

<blockquote>
  <p><strong>Note:</strong></p>
  
  <ul>
  <li>N’hésitez pas à déclarer des bug en ouvrant des issues. Si vous avez une idée de fonctionnalité, faites une pull request ou codez-la vous même !</li>
  <li>Si vous voulez participer au développement, n’hésiter pas à ouvrir une branch !</li>
  </ul>
</blockquote>

<hr>



<h2 id="screenshots">Screenshots</h2>

<p><img src="http://i.imgur.com/LkZlyng.png" alt="Profil" title=""> <img src="http://i.imgur.com/ncBgKTW.png" alt="Projet" title="Projet"> <img src="http://i.imgur.com/0YkixGX.png" alt="Trombi" title=""> <img src="http://i.imgur.com/JPO9ZUj.png" alt="Planning" title="Planning">  <img src="http://i.imgur.com/QjDqxwm.png" alt="Notifications" title="Notifications"> <img src="http://i.imgur.com/zGwkWsK.png" alt="E-Learning" title="E-Learning"></p>



<h2 id="fonctionnalités">Fonctionnalités</h2>

<ul>
<li>Accès à tous les profils, notes et log des étudiants du groupe IONIS.</li>
<li>Possibilité de voir toutes les notes pour une activités, avec les commentaires.</li>
<li>Gestion du planning étudiant (inscription | désinscription | validation du token).</li>
<li>Voir qui est inscrit à telle ou telle sessions. Voir quel profs gèrent telle activité.</li>
<li>Synchronisation optionnelle de votre planning avec l’application calendrier (seulement les événements auxquelles vous êtes inscrit).</li>
<li>Accès aux projets en cours, aux groupes et aux sujets des projets.</li>
<li>Recherche avec auto-complétion parmi les membres du groupe IONIS.</li>
<li>Gestion des notifications de l’intra (avec pop-up sur le Device).</li>
<li>Accès aux ressources d’E-Learning (y compris les vidéos). Pratique quand internet flanche pendant la piscine.</li>
<li>Actualisation des données en arrière-plan.</li>
<li>Interface fluide et réactive.</li>
<li>Gestion sécurisé des identifiants via Keychain. Protection par TouchID si disponible. (Parce qu’un password en clair dans les fichiers d’une app, ça fait mauvais effet…)</li>
<li>Toutes les orientations supportés. Toute les tailles d’écran supportés.</li>
<li>Support de l’iPad</li>
</ul>

<hr>



<h2 id="le-code">Le code</h2>

<p>La solutions se compose de trois projets :</p>

<ul>
<li><strong><em>Epitech.Intra.API :</em></strong> Contiens le code métier de l’application, les  classes de donnés et les points d’entrés de l’API.</li>
<li><strong><em>Epitech.Intra.SharedApp :</em></strong> Contiens le code partagé de toutes les applications iOS et Android. Défini les interfaces et les mécaniques d’utilisation.</li>
<li><strong><em>Epitech.Intra.iOS :</em></strong> Contiens l’application iOS. Utile exclusivement pour les appels aux APIs spécifiques au device, comme la  gestion de la Keychain ou EventKit pour iOS.</li>
</ul>

<blockquote>
  <p><strong>Note:</strong></p>
  
  <p>Vous devez compiler OxyPlot avec la solution. <a href="https://github.com/oxyplot/oxyplot">OxyPlot</a> est un super projet open-source pour tracer des graph. Vous pouvez aussi utiliser le paquet NuGet.</p>
</blockquote>



<h4 id="ajouter-une-fonctionnalité-dapi"><i class="icon-pencil"> Ajouter une fonctionnalité d’API</i></h4>

<p>Voici un exemple d’appel à l’intra pour récupérer le json de la page d’accueil :</p>



<pre class="prettyprint prettyprinted"><code><span class="pln">    </span><span class="kwd">public</span><span class="pln"> async </span><span class="typ">Task</span><span class="pun">&lt;</span><span class="typ">Welcome</span><span class="pun">&gt;</span><span class="pln"> </span><span class="typ">GetWelcome</span><span class="pln"> </span><span class="pun">()</span><span class="pln">
    </span><span class="pun">{</span><span class="pln">
        </span><span class="com">//Déclaration du client http.</span><span class="pln">
        </span><span class="typ">HttpClient</span><span class="pln"> client </span><span class="pun">=</span><span class="pln"> </span><span class="kwd">new</span><span class="pln"> </span><span class="typ">HttpClient</span><span class="pln"> </span><span class="pun">();</span><span class="pln">

        </span><span class="kwd">try</span><span class="pln"> </span><span class="pun">{</span><span class="pln">
            </span><span class="com">//Envoi de la requête (GetHeader : génération d'un header avec les identifiants de l'utilisateur).</span><span class="pln">
            </span><span class="kwd">var</span><span class="pln"> result </span><span class="pun">=</span><span class="pln"> await client</span><span class="pun">.</span><span class="typ">PostAsync</span><span class="pln"> </span><span class="pun">(</span><span class="pln">buildUri </span><span class="pun">(</span><span class="str">"/"</span><span class="pun">),</span><span class="pln"> </span><span class="typ">GetHeader</span><span class="pln"> </span><span class="pun">());</span><span class="pln">
            </span><span class="com">//On retourne l'objet JSON désérialisé via Json.Net ou null si l'appel n'a pas abouti.</span><span class="pln">
            </span><span class="kwd">return</span><span class="pln"> </span><span class="pun">!</span><span class="pln">result</span><span class="pun">.</span><span class="typ">IsSuccessStatusCode</span><span class="pln"> </span><span class="pun">?</span><span class="pln"> </span><span class="kwd">null</span><span class="pln"> </span><span class="pun">:</span><span class="pln"> </span><span class="typ">Newtonsoft</span><span class="pun">.</span><span class="typ">Json</span><span class="pun">.</span><span class="typ">JsonConvert</span><span class="pun">.</span><span class="typ">DeserializeObject</span><span class="pun">&lt;</span><span class="typ">Welcome</span><span class="pun">&gt;</span><span class="pln"> </span><span class="pun">(</span><span class="pln">await result</span><span class="pun">.</span><span class="typ">Content</span><span class="pun">.</span><span class="typ">ReadAsStringAsync</span><span class="pln"> </span><span class="pun">());</span><span class="pln">
        </span><span class="pun">}</span><span class="pln"> </span><span class="kwd">catch</span><span class="pln"> </span><span class="pun">(</span><span class="typ">Exception</span><span class="pln"> e</span><span class="pun">)</span><span class="pln"> </span><span class="pun">{</span><span class="pln">
            </span><span class="com">//Si il y a une erreur, on la throw sur la pile d'appel.</span><span class="pln">
            </span><span class="kwd">throw</span><span class="pln"> </span><span class="kwd">new</span><span class="pln"> </span><span class="typ">Exception</span><span class="pln"> </span><span class="pun">(</span><span class="str">"Impossible de récupérer le message de bienvenue"</span><span class="pun">,</span><span class="pln"> e</span><span class="pun">);</span><span class="pln">
        </span><span class="pun">}</span><span class="pln">
    </span><span class="pun">}</span></code></pre>

<p>Cette fonction demande la ressource “/” (c’est à dire <a href="http://intra.epitech.eu">http://intra.epitech.eu</a><strong>/</strong>), puis converti le json de la réponse en un objet <code>Welcome</code> via la fonction <code>DeserializeObject(string jsoncontent)</code>. Les appels susceptibles de générer des erreurs sont entourés de try-catch. Les exceptions sont renvoyés à la fonction appelante si lieu.</p>

<blockquote>
  <p><strong>Note:</strong></p>
  
  <p>La fonction retourne un type <code>Task&lt;Welcome&gt;</code>, elle est donc non bloquante. Toute les fonction de l’API doivent respecter ce format.</p>
</blockquote>

<p>Maintenant, il ne reste plus qu’a consumer l’API via ces fonctions.</p>



<h4 id="le-système-de-page-et-les-appels-à-lapi"><i class="icon-file"></i> Le système de page et les appels à l’API</h4>

<p>Toutes les pages d’interface utilisateur (présentent dans <em>Epitech.Intra.SharedApp.Views</em>) héritent de la classe <code>IntraPage</code> qui définit la gestion des données, l’invalidation et qui appelle les fonctions d’API si besoin. Son fonctionnement ressemble à ceci :</p>



<div class="sequence-diagram"><svg height="376" version="1.1" width="578" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="overflow: hidden; position: relative; left: -0.5px; top: -0.234375px;"><desc style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">Created with Raphaël 2.1.2</desc><defs style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><path stroke-linecap="round" d="M5,0 0,2.5 5,5z" id="raphael-marker-block" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><marker id="raphael-marker-endblock55-obj41" markerHeight="5" markerWidth="5" orient="auto" refX="2.5" refY="2.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="#raphael-marker-block" transform="rotate(180 2.5 2.5) scale(1,1)" stroke-width="1.0000" fill="#000" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></use></marker><marker id="raphael-marker-endblock55-obj44" markerHeight="5" markerWidth="5" orient="auto" refX="2.5" refY="2.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="#raphael-marker-block" transform="rotate(180 2.5 2.5) scale(1,1)" stroke-width="1.0000" fill="#000" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></use></marker><marker id="raphael-marker-endblock55-obj47" markerHeight="5" markerWidth="5" orient="auto" refX="2.5" refY="2.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="#raphael-marker-block" transform="rotate(180 2.5 2.5) scale(1,1)" stroke-width="1.0000" fill="#000" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></use></marker><marker id="raphael-marker-endblock55-obj50" markerHeight="5" markerWidth="5" orient="auto" refX="2.5" refY="2.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="#raphael-marker-block" transform="rotate(180 2.5 2.5) scale(1,1)" stroke-width="1.0000" fill="#000" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></use></marker><marker id="raphael-marker-endblock55-obj53" markerHeight="5" markerWidth="5" orient="auto" refX="2.5" refY="2.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="#raphael-marker-block" transform="rotate(180 2.5 2.5) scale(1,1)" stroke-width="1.0000" fill="#000" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></use></marker><marker id="raphael-marker-endblock55-obj56" markerHeight="5" markerWidth="5" orient="auto" refX="2.5" refY="2.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="#raphael-marker-block" transform="rotate(180 2.5 2.5) scale(1,1)" stroke-width="1.0000" fill="#000" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></use></marker><marker id="raphael-marker-endblock55-obj59" markerHeight="5" markerWidth="5" orient="auto" refX="2.5" refY="2.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"><use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="#raphael-marker-block" transform="rotate(180 2.5 2.5) scale(1,1)" stroke-width="1.0000" fill="#000" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></use></marker></defs><rect x="10" y="20" width="54" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="20" y="30" width="34" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="37" y="38.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">Page</tspan></text><rect x="10" y="319" width="54" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="20" y="329" width="34" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="37" y="337.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">Page</tspan></text><path fill="none" stroke="#000000" d="M37,57L37,319" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="213.5" y="20" width="83" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="223.5" y="30" width="63" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="255" y="38.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">IntraPage</tspan></text><rect x="213.5" y="319" width="83" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="223.5" y="329" width="63" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="255" y="337.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">IntraPage</tspan></text><path fill="none" stroke="#000000" d="M255,57L255,319" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="365" y="20" width="44" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="375" y="30" width="24" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="387" y="38.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">API</tspan></text><rect x="365" y="319" width="44" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="375" y="329" width="24" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="387" y="337.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">API</tspan></text><path fill="none" stroke="#000000" d="M387,57L387,319" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="429" y="20" width="119" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="439" y="30" width="99" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="488.5" y="38.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">intra.epitech.eu</tspan></text><rect x="429" y="319" width="119" height="37" rx="0" ry="0" fill="none" stroke="#000000" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><rect x="439" y="329" width="99" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="488.5" y="337.5" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">intra.epitech.eu</tspan></text><path fill="none" stroke="#000000" d="M488.5,57L488.5,319" stroke-width="2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="105" y="73.5" width="82" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="146" y="82" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">InitIntraPage</tspan></text><path fill="none" stroke="#000000" d="M37,94C37,94,217.44250550866127,94,249.9979282554632,94" stroke-width="2" marker-end="url(#raphael-marker-endblock55-obj41)" stroke-dasharray="6,2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="104.5" y="110.5" width="83" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="146" y="119" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">RefreshData</tspan></text><path fill="none" stroke="#000000" d="M37,131C37,131,217.44250550866127,131,249.9979282554632,131" stroke-width="2" marker-end="url(#raphael-marker-endblock55-obj44)" stroke-dasharray="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="265" y="147.5" width="112" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="321" y="156" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">Appel de fonction</tspan></text><path fill="none" stroke="#000000" d="M255,168C255,168,357.9321520328522,168,382.0066153256921,168" stroke-width="2" marker-end="url(#raphael-marker-endblock55-obj47)" stroke-dasharray="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="413.25" y="184.5" width="49" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="437.75" y="193" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">requête</tspan></text><path fill="none" stroke="#000000" d="M387,205C387,205,463.0717853307724,205,483.50121986784507,205" stroke-width="2" marker-end="url(#raphael-marker-endblock55-obj50)" stroke-dasharray="6,2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="0" y="0" width="0" height="0" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="437.75" y="230" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="230" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></tspan></text><path fill="none" stroke="#000000" d="M488.5,225C488.5,225,412.4282146692276,225,391.99878013215493,225" stroke-width="2" marker-end="url(#raphael-marker-endblock55-obj53)" stroke-dasharray="6,2" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="265.5" y="241.5" width="111" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="321" y="250" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">réponse de l'intra</tspan></text><path fill="none" stroke="#000000" d="M387,262C387,262,284.0678479671478,262,259.9933846743079,262" stroke-width="2" marker-end="url(#raphael-marker-endblock55-obj56)" stroke-dasharray="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path><rect x="47" y="278.5" width="198" height="17" rx="0" ry="0" fill="#ffffff" stroke="none" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></rect><text x="146" y="287" text-anchor="middle" font-family="Andale Mono, monospace" font-size="16px" stroke="none" fill="#000000" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0); text-anchor: middle; font-family: 'Andale Mono', monospace; font-size: 16px;"><tspan dy="5.5" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);">DisplayContent || DisplayError</tspan></text><path fill="none" stroke="#000000" d="M255,299C255,299,74.55749449133873,299,42.002071744536806,299" stroke-width="2" marker-end="url(#raphael-marker-endblock55-obj59)" stroke-dasharray="0" style="-webkit-tap-highlight-color: rgba(0, 0, 0, 0);"></path></svg></div>

<p>Pour l’utiliser, il faut d’abord appeller la fonction </p>



<pre class="prettyprint prettyprinted"><code><span class="typ">InitIntraPage</span><span class="pln"> </span><span class="pun">(</span><span class="typ">Type</span><span class="pln"> type</span><span class="pun">,</span><span class="pln"> </span><span class="typ">Func</span><span class="pun">&lt;</span><span class="typ">Task</span><span class="str">&lt;object&gt;</span><span class="pun">&gt;</span><span class="pln"> </span><span class="kwd">function</span><span class="pun">,</span><span class="pln"> </span><span class="typ">TimeSpan</span><span class="pln"> dataInvalidation</span><span class="pun">);</span></code></pre>

<p>Le premier paramètre est un typeof de la classe de la page, le second un pointeur sur la fonction d’API voulu et le troisième un <code>TimeSpan</code> d’invalidation des données. Une surcharge de cette fonction existe et permet d’ajouter un quatrième paramètre pour passer une string à la fonction d’API. Par exemple pour la page du profil l’appel est :</p>



<pre class="prettyprint prettyprinted"><code><span class="com">// Ma page est de type "Profile", la fonction est GetUser, les données s'invalident après 1 heure et le login de mon utilisateur est TargetUser.</span><span class="pln">
</span><span class="typ">InitIntraPage</span><span class="pln"> </span><span class="pun">(</span><span class="kwd">typeof</span><span class="pun">(</span><span class="typ">Profile</span><span class="pun">),</span><span class="pln"> </span><span class="typ">App</span><span class="pun">.</span><span class="pln">API</span><span class="pun">.</span><span class="typ">GetUser</span><span class="pun">,</span><span class="pln"> </span><span class="kwd">new</span><span class="pln"> </span><span class="typ">TimeSpan</span><span class="pln"> </span><span class="pun">(</span><span class="lit">1</span><span class="pun">,</span><span class="pln"> </span><span class="lit">0</span><span class="pun">,</span><span class="pln"> </span><span class="lit">0</span><span class="pun">),</span><span class="pln"> </span><span class="typ">TargetUser</span><span class="pun">);</span></code></pre>

<p><strong>L’appel à cette fonction doit se faire avant tout autre, de préférence dans le constructeur de la classe.</strong></p>

<p>La classe <code>IntraPage</code> définit deux fonctions pouvant être <strong>override</strong> par la classe fille :</p>

<ul>
<li><code>public virtual void DisplayContent (object data)</code>, qui est automatiquement appelé lorsque le contenu de la page est disponible. Data contiens les donnée retourné par l’API. L’objet doit être casté pour être utilisé. C’est dans cette fonction qu’il faudra creer/mettre à jour la page avec les nouvelles data.</li>
<li><code>public virtual void DisplayError (Exception ex)</code>, qui permet de customiser l’affiche par défaut des erreurs. Ceci est optionnel.</li>
</ul>

<p>Elle définit aussi deux fonctions publiques :</p>

<ul>
<li><code>public async Task RefreshData (bool forceFetch, string dat)</code>, qui demande à renouveler les donnée de la page. forceFetch permet de forcer l’appel à l’API (outrepasser l’invalidation automatique), le second permet de passer une string à la fonction d’API. Cette fonction affiche automatiquement sur la page un <code>ActivityIndicator</code>pour signaler à l’utilisateur le chargement. Si les données sont toujours valides, alors <code>DisplayContent</code> sera immédiatement appelé avec les données présentent en local.</li>
<li><code>public virtual async Task&lt;object&gt; SilentUpdate (string param)</code>, qui permet la même chose que <code>RefreshData</code> mais est silencieuse, elle n’appellera pas <code>DisplayContent</code>et ne changera pas le contenu de la page. Au contraire, elle retourne directement les données demandé. Elle est très utile pour le <code>PullToRefresh</code> d’une liste  par exemple. Cette fonction peut aussi être override pour des cas particuliers si besoin.</li>
</ul>

<p>Ainsi, pour obtenir les données d’une page, il faut appeler la fonction <code>RefreshData</code>. Cette fonction se charge de tout : verifier l’invalidation des données locale si lieu et fetch via l’API REST d’Epitech si besoin. Une fois les données disponible, <code>RefreshData</code> appellera automatiquement la fonction <code>DisplayContent</code> et il ne vous reste plus qu’à afficher les données. Si une erreur se produit, elle appellera <code>DisplayError</code>.</p>

<blockquote>
  <p><strong>Note:</strong> <br>
  Pour toujours afficher à l’utilisateur les données les plus récentes,  il est conseillé d’overrider la fonction <code>OnAppearing ()</code>des pages et d’y mettre son appel à <code>RefreshData</code>.</p>
</blockquote>

<p>Ce système permet de créer des pages <strong><em>très vite</em></strong> sans ce soucier de comment les données sont gérés, ou même de la gestions des erreurs de l’API, l’objet <code>IntraPage</code> le gère une fois pour toute. De plus, il permet de garder dans les pages <strong>un code simple et concis, orienté sur les données</strong>, le travail compliqué ce faisant ailleurs.</p>

<hr>



<h2 id="contact">Contact</h2>

<p>Je suis joignable à l’adresse <strong><em><a href="mailto:hippolyte.barraud@epitech.eu?subject=%5BMicroNet%5D">hippolyte.barraud@epitech.eu</a></em></strong> si vous avez des questions sur l’app ou sur le développement via Xamarin, ou plus généralement sur c# ! </p>

<blockquote>
  <p><strong>Important:</strong></p>
  
  <p>Si quelqu’un est doué en graphisme, ou plus généralement à un gout pour l’esthétisme plus prononcé que moi (ce sera pas dûr), il est le bienvenu ! Actuellement, la beauté du code ne se reflète pas vraiment sur l’interface… <img src="http://www.freesmileys.org/smileys/smiley-sad014.gif" alt="enter image description here" title=""></p>
</blockquote>

<hr>



<h2 id="index">Index</h2>

<p><div class="toc">
<ul>
<li><a href="#epitech-micronet">Epitech MicroNet</a><ul>
<li><a href="#screenshots">Screenshots</a></li>
<li><a href="#fonctionnalités">Fonctionnalités</a></li>
<li><a href="#le-code">Le code</a><ul>
<li><ul>
<li><a href="#ajouter-une-fonctionnalité-dapi"> Ajouter une fonctionnalité d’API</a></li>
<li><a href="#le-système-de-page-et-les-appels-à-lapi"> Le système de page et les appels à l’API</a></li>
</ul>
</li>
</ul>
</li>
<li><a href="#contact">Contact</a></li>
<li><a href="#index">Index</a></li>
</ul>
</li>
</ul>
</div>
</p>