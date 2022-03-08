using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using TMPro;
using UnityEngine;

public class FakeSecondaryParatext : MonoBehaviour
{
    private string paratext =
        "In the School of Athens, Plato and Aristotle command a wide assembly of philosophers. Pointing heavenward to the realm of Ideas, Plato carries the Timaeus. His student Aristotle strides beside him holding the Nicomachean Ethics and extends his hand over the rational world. Julius II owned John Argyropoulos’s translation of the Ethics, as well as its companion, the Politics, " +
        "translated by Leonardo Bruni. Although the library’s inventory lists no Platonic texts (and only one work in Greek), that the volume is one of seven titles pictured in the Stanza suggests that the papal library also included the Timaeus. \n\n" +
        "The Timaeus marks the fresco’s vanishing point and its prominence in the painting points to the significance of Plato’s dialogue for the formal organization of Raphael’s compositions. Scholars have often noted that the figure to the right of the altar in the Disputa echoes Plato in gesture and form, serving as a philosophical bridge between the frescoes’ programs. Indeed, the landscape of Raphael’s Theology echoes the geometries that govern Plato’s text. Ascending in three hierarchical planes, the visual order of the Disputa gives shape to Neoplatonic metaphysics and the esoteric principle of emanation. According to Plotinus, the so-called father of Neoplatonism, emanation describes the process of intellectual contemplation, as it extends from the material world to the divine One. On the lowest level of the fresco, which is characterized by rigorous perspective, Christian theologians gather to bear witness to the Sacrament of the Eucharist. Above, the Judeo-Christian patriarchs occupy an ethereal bank of clouds comprised of materializing putti, which departs from the strict spatial laws of the terrestrial plane below. In the highest, intellectual realm, the golden disk of heaven is characterized by seraphim and corresponds to the incommensurable domain of the divine. " +
        "\n\nThe fresco’s three levels also evoke the Platonic geometries maintained in the Timaeus. Like the Pythagoreans, Plato considered three the mathematical basis for the soul, and in the Republic, he described its tripartite nature, including the appetite (corresponding to desire), the spirit (honor), and reason (truth). In the Timaeus, the anima mundi or “world soul,” which the Demiurge set midway between the indivisible, the transient, and the material (35A), runs parallel to this structure. What is more, the geometry of the circle played an important role in Plato’s theories on harmony and the cosmos, echoing the spheres in Vigerio’s manuscript. As Plato explains in the Timaeus, the universe is an ordered production of concentric circles whose movement is governed by God’s intellect (34A-34B). And so in the Disputa, circles transcend the imagined planes of existence and hierarchically organize the spatial ordering of the fresco: along the painting’s vertical axis, God holds the orb of the world––a parallel to the celestial and earthly spheres presented by Zoroaster and Ptolemy in the School of Athens; a sun-like mandorla, framed by colorfully-winged putti heads, encircles Christ; a circular burst of golden light encompasses the Holy Spirit just below Christ; and below, the wafer of the Eucharist is surrounded by the circular monstrance. Four implied circles are also evident: the hemispherical heavenly rays designate the intellectual realm against which God’s personage is staged; the Fathers of the Church are enthroned upon a hemicycle of clouds in the celestial realm; the theologians congregate in the shape of a hemicycle around the Eucharist in the terrestrial world; and finally, the semicircular architectural frame opens onto Raphael’s stage. " +
        "\n\nThe Platonic themes embodied by Raphael’s frescoes also evoke the literature of theologians past and present. Augustine, who maintained the Neoplatonic principle of emanation, sits to the right of the altar in the Disputa. A disciple of Ficino’s Platonic Theology, the Augustinian friar Egidio da Viterbo—often thought to have served as one of Raphael’s advisors—drew upon these ideas in his Sententiae ad mentem Platonis. And finally, Marco Vigerio della Rovere embraced aspects of Plato’s cosmology in his long exegesis on Christian faith.";

    public TMP_Text interleaf;

    public GameObject backButton;

    public ScalarCamera cam;
    
    public Transform cameraPos;
    public Transform targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        TMP_TextEventHandler.OnDetailLinkSelected += HandleClicks;
        backButton.SetActive(false);
    }

    private void HandleClicks(string tag)
    {
        if (tag == "plato_image")
        {
            interleaf.text = paratext;
            backButton.SetActive(true);
        }
    }

    public void OnBackButtonClicked()
    {
        
        backButton.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
