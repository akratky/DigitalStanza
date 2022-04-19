using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using TMPro;
using UnityEngine;
using VLB;

public class FakeSecondaryParatext : MonoBehaviour
{
    private string paratext =
        "In the School of Athens, Plato and Aristotle command a wide assembly of philosophers. Pointing heavenward to the realm of Ideas, Plato carries the Timaeus. His student Aristotle strides beside him holding the Nicomachean Ethics and extends his hand over the rational world. Julius II owned John Argyropoulos’s translation of the Ethics, as well as its companion, the Politics, " +
        "translated by Leonardo Bruni. Although the library’s inventory lists no Platonic texts (and only one work in Greek), that the volume is one of seven titles pictured in the Stanza suggests that the papal library also included the Timaeus. \n\n" +
        "The Timaeus marks the fresco’s vanishing point and its prominence in the painting points to the significance of Plato’s dialogue for the formal organization of Raphael’s compositions. Scholars have often noted that the figure to the right of the altar in the Disputa echoes Plato in gesture and form, serving as a philosophical bridge between the frescoes’ programs. Indeed, the landscape of Raphael’s Theology echoes the geometries that govern Plato’s text. Ascending in three hierarchical planes, the visual order of the Disputa gives shape to Neoplatonic metaphysics and the esoteric principle of emanation. According to Plotinus, the so-called father of Neoplatonism, emanation describes the process of intellectual contemplation, as it extends from the material world to the divine One. On the lowest level of the fresco, which is characterized by rigorous perspective, Christian theologians gather to bear witness to the Sacrament of the Eucharist. Above, the Judeo-Christian patriarchs occupy an ethereal bank of clouds comprised of materializing putti, which departs from the strict spatial laws of the terrestrial plane below. In the highest, intellectual realm, the golden disk of heaven is characterized by seraphim and corresponds to the incommensurable domain of the divine. " +
        "\n\nThe fresco’s three levels also evoke the Platonic geometries maintained in the Timaeus. Like the Pythagoreans, Plato considered three the mathematical basis for the soul, and in the Republic, he described its tripartite nature, including the appetite (corresponding to desire), the spirit (honor), and reason (truth). In the Timaeus, the anima mundi or “world soul,” which the Demiurge set midway between the indivisible, the transient, and the material (35A), runs parallel to this structure. What is more, the geometry of the circle played an important role in Plato’s theories on harmony and the cosmos, echoing the spheres in Vigerio’s manuscript. As Plato explains in the Timaeus, the universe is an ordered production of concentric circles whose movement is governed by God’s intellect (34A-34B). And so in the Disputa, circles transcend the imagined planes of existence and hierarchically organize the spatial ordering of the fresco: along the painting’s vertical axis, God holds the orb of the world––a parallel to the celestial and earthly spheres presented by Zoroaster and Ptolemy in the School of Athens; a sun-like mandorla, framed by colorfully-winged putti heads, encircles Christ; a circular burst of golden light encompasses the Holy Spirit just below Christ; and below, the wafer of the Eucharist is surrounded by the circular monstrance. Four implied circles are also evident: the hemispherical heavenly rays designate the intellectual realm against which God’s personage is staged; the Fathers of the Church are enthroned upon a hemicycle of clouds in the celestial realm; the theologians congregate in the shape of a hemicycle around the Eucharist in the terrestrial world; and finally, the semicircular architectural frame opens onto Raphael’s stage. " +
        "\n\nThe Platonic themes embodied by Raphael’s frescoes also evoke the literature of theologians past and present. Augustine, who maintained the Neoplatonic principle of emanation, sits to the right of the altar in the Disputa. A disciple of Ficino’s Platonic Theology, the Augustinian friar Egidio da Viterbo—often thought to have served as one of Raphael’s advisors—drew upon these ideas in his Sententiae ad mentem Platonis. And finally, Marco Vigerio della Rovere embraced aspects of Plato’s cosmology in his long exegesis on Christian faith.";

    
    private string lyreParatex =
        "Raphael depicted the lyre in its various forms in the Stanza della Segnatura, drawing upon contemporary discussions regarding how the ancient lyre and its modern counterpart, the lira da braccio, were formed and played. For example, Raffaelle Brandolini, a humanist in Julius II's court, wrote that the lyre \"was invented by Mercury..., either with three strings, for the three changes of time, or with four strings in order to indicate plainly the four elements....Coroebus, son of Atys the Lydian, invented the fifth string, Hyagnis the Phrygian the sixth, Apollo or Terpander the seventh, in order to indicate well the number of the planets...Simonides...added the eighth string to the lyre.\"" +
        "\n\nOn the Parnassus wall, Sappho's Aeolian lyre of barbitos has the shape of the instrument described in the fourth Homeric Hymn, made by Mercury from the shell of a tortoise, though Raphael shows it with five strings, rather than the Homeric seven strings. The shape of the instrument follows those shown on the ancient Mattei sarcophagus now in the Museo Nazionale di Roma. Sappho holds a plectrum for plucking the strings in her hand. The muse often identified as Terpsichore holds an antique kithara with seven strings and two gracefully curved arms. The shape of Terpsichore's instrument, like that of Sappho, imitates those shown on the ancient Mattei sarcophagus. Apollo, seated among the nine muses, plays a Renaissance instrument known as the lira da braccio (indicating its position, held by the arm) or lira moderna (\"modern lyre\"). Its bow, heart-shaped pegboard and free strings are Renaissance adaptations of the ancient Apollonian instrument that its name, lira, explicitly recalls. Raphael shows Apollo's lira da braccio with a total of nine strings, the number that the late-sixteenth-century music theorist (and father of astronomer Galileo) Vincenzo Galilei wrote, \"The strings of [the lyre] were ... assigned to the choir of the nine muses.\""
    +
    "\n\nRaphael’s musical catalog extends to Theology. In the Disputa, King David sits in the upper tier of holy personages, turning his head towards the Parnassus, and holding a nine-stringed zither or psaltery with which, according to the Biblical text, he soothed Saul. The soundbox of David's instrument has a shape similar to the illustrations in the Decachordum (fols. 7r and 8r), although the latter examples have ten strings. By the end of the fifteenth century, David was more often represented with the lira da braccio, and so in both the manuscript and the painting, the inclusion of the psaltery – whose performance was associated with singing the psalms, and which saw a steep decline in production after 1500 – suggests a shared reference. "
    +
    "\n\nAncient definitions of harmony relating to music similarly inform Raphael’s representation of Philosophy. Pythagoras appears in the foreground of the School of Athens, where he studies a tablet illustrating the harmonic scale he is credited with discovering. The intervals imagined on the philosopher’s slate include the octave (“diapason”), the fifth (“diapente”), the fourth (diatessaron), and the whole tone. Summarized by the triangular figure at the bottom of Raphael’s diagram, the numbers comprising these ratios (I, II, III, and IV) together made ten and were called the tetraktys by Pythagoreans. Considering ten a perfect number, the Pythagoreans associated the principle of its harmony with the movements of the cosmos. Plato maintained a similar position in the Timaeus, the same book he carries in Raphael’s fresco. In the Timaeus, as in the Republic, he suggested that the harmonic nature of music resonates in the universe and the soul. This relationship is evoked on the Segnatura ceiling, where Astronomy (Urania) spins the celestial spheres as they hum in perfect concert. In Plato’s vision of the universe, described in the Republic, eight heavenly spheres rotate on eight whorls around the spindle of Necessity; as they turn at equal intervals, eight sirens sing a single scale in harmony. ";

    private string mainText;

    public GameObject interleafObj;
    public TMP_Text interleaf;
    public TMP_Text interleafHeader;

    public TMP_Text currentPageText;
    public TMP_Text totalPageText;
    
    public GameObject backButton;

    public ScalarCamera cam;
    
    public Transform cameraPos;
    public Transform targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        TMP_TextEventHandler.OnDetailLinkSelected += HandleClicks;
        backButton.SetActive(false);
        interleafObj.SetActive(false);
    }

    private void HandleClicks(string tag)
    {
        Debug.Log("Tag: " + tag);
        if (tag == "Plato")
        {
            mainText = interleaf.text;
            interleafHeader.text = "Plato";
            interleaf.text = paratext;
            backButton.SetActive(true);
            interleafObj.SetActive(true);
            

        }

        if (tag == "lyre")
        {
            mainText = interleaf.text;
            interleafHeader.text = "The Lyre in Word and Image";
            interleaf.text = lyreParatex;
            backButton.SetActive(true);
            interleafObj.SetActive(true);

        }
        
        UpdateUI();
    }

    public void OnNextSecondInterleafPage()
    {
        if (interleaf.pageToDisplay != interleaf.textInfo.pageCount)
        {
            interleaf.pageToDisplay++;
        }
        
        UpdateUI();
    }

    public void OnPrevInterleafPage()
    {
        if (interleaf.pageToDisplay > 0)
        {
            interleaf.pageToDisplay--;
        }
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        currentPageText.text = (interleaf.pageToDisplay + 1).ToString();
        totalPageText.text = (interleaf.textInfo.pageCount + 1).ToString();
    }
    
    
    
    public void OnBackButtonClicked()
    {
        BookLineRenderer.DestroyLineEvent.Invoke();
        interleafObj.SetActive(!interleafObj.activeSelf);

    }

}
